using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
public class Map : IMap
{
    private static readonly PlantType[] SlotPlantTypes =
        { PlantType.Peashooter, PlantType.Sunflower, PlantType.SnowPea, PlantType.Repeater,PlantType.Chomper,PlantType.WallNut,PlantType.CherryBomb,PlantType.PotatoMine };

    private static readonly string[] SeedPacketAssetNames =
        { "peashooter_seedpacket", "sunflower_seedpacket", "snowpea_seedpacket", "repeater_seedpacket", "chomper_seedpacket","wallnut_seedpacket", "bomb_seedpacket", "mine_seedpacket" };

    private readonly Texture2D _background;
    private readonly Texture2D _pixel;
    private readonly SeedSlot _seedSlot;
    private readonly Texture2D _shovelTexture;
    private readonly Rectangle _shovelBounds;
    private readonly PlantFactory _plantFactory;
    private List<Projectile> _projectiles = new();
    private Texture2D _peaTexture;
    private Texture2D _snowPeaTexture;
    public readonly GridManager _grid;
    public IReadOnlyList<Projectile> Projectiles => _projectiles;

    private const int ScreenWidth = 800;
    private const int ScreenHeight = 600;
    private const int TrayX = 10;
    private const int TrayY = 8;
    private const int TrayWidth = 700;
    private const int ShovelSize = 70;

    private const int GridRows = 5;
    private const int GridCols = 9;
    private const int CellWidth = 80;
    private const int CellHeight = 90;
    private const int GridOriginX = 40;
    private const int GridOriginY = 100;

    public PlantType? SelectedPlantType { get; private set; }

    public Map(ContentManager content, GraphicsDevice device)
    {
        

        _pixel = new Texture2D(device, 1, 1);
        _pixel.SetData(new[] { Color.White });

        _background = content.Load<Texture2D>("day_level");
        _shovelTexture = content.Load<Texture2D>("shovel");
        _peaTexture = content.Load<Texture2D>("pea_projectile");
        _snowPeaTexture = content.Load<Texture2D>("snowpea_projectile");
        _plantFactory = new PlantFactory(_projectiles, _peaTexture, _snowPeaTexture);
        _plantFactory.LoadContent(content);  

        var trayTexture = content.Load<Texture2D>("seedslot");
        var packetTextures = new Texture2D[SlotPlantTypes.Length];
        for (int i = 0; i < SlotPlantTypes.Length; i++)
            packetTextures[i] = content.Load<Texture2D>(SeedPacketAssetNames[i]);

        _seedSlot = new SeedSlot(trayTexture, packetTextures, _pixel, TrayX, TrayY);

        int shovelX = TrayX + TrayWidth + 8;
        int shovelY = TrayY + (_seedSlot.TrayBounds.Height - ShovelSize) / 2;
        _shovelBounds = new Rectangle(shovelX, shovelY, ShovelSize, ShovelSize);

        _grid = new GridManager();
        _grid.Initialize(GridRows, GridCols, CellWidth, CellHeight, new Point(GridOriginX, GridOriginY));
    }

    public void SelectPlant(PlantType type)
    {
        SelectedPlantType = type;
    }

    public void ClearPlant()
    {
        SelectedPlantType = null;
    }

    public bool TryPlacePlantAt(Point screenPos)
    {
        if (!SelectedPlantType.HasValue)
            return false;

        var plot = _grid.GetPlotAt(screenPos);
        if (plot == null || plot.IsOccupied)
            return false;

        var plant = _plantFactory.Create(SelectedPlantType.Value,
            plot.Position.X, plot.Position.Y);
        if (plant == null)
            return false;

        plot.PlacePlant(plant);
        return true;
    }

    public bool TryRemovePlantAt(Point screenPos)
    {
        var plot = _grid.GetPlotAt(screenPos);
        if (plot == null || !plot.IsOccupied)
            return false;

        plot.RemovePlant();
        return true;
    }

    public int GetSeedPacketIndexAt(Point screenPos)
    {
        return _seedSlot.HitTestPacket(screenPos.X, screenPos.Y);
    }

    public bool IsShovelAt(Point screenPos)
    {
        return _shovelBounds.Contains(screenPos);
    }

    public PlantType? GetPlantTypeForSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= SlotPlantTypes.Length)
            return null;
        return SlotPlantTypes[slotIndex];
    }

    public void Update(GameTime gameTime)
    {
        foreach (var plot in _grid.AllPlots)
        {
            if (plot.IsOccupied && plot.Plant is IPvZUpdatable updatable)
                updatable.Update(gameTime);
        }
        foreach (var projectile in _projectiles)
            {
                projectile.Update(gameTime);
            }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawBackground(spriteBatch);

        foreach (var plot in _grid.AllPlots)
            plot.Draw(spriteBatch);

        int selectedIndex = -1;
        if (SelectedPlantType.HasValue)
        {
            for (int i = 0; i < SlotPlantTypes.Length; i++)
            {
                if (SlotPlantTypes[i] == SelectedPlantType.Value)
                {
                    selectedIndex = i;
                    break;
                }
            }
        }
        _seedSlot.Draw(spriteBatch, selectedIndex);

        spriteBatch.Draw(_shovelTexture, _shovelBounds, Color.White);
        foreach (var projectile in _projectiles)
        {
            projectile.Draw(spriteBatch);
        }
    }

    private void DrawBackground(SpriteBatch spriteBatch)
    {
        var sourceRect = new Rectangle((int)(_background.Width * 0.15f), 0, (int)(_background.Width * 0.58f), _background.Height);
        spriteBatch.Draw(_background, new Rectangle(0, 0, ScreenWidth, ScreenHeight), sourceRect, Color.White);
    }
}
