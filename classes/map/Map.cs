using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprites;
using System.Collections.Generic;
using SysPoint = System.Drawing.Point;

public class Map : IMap
{
    private static readonly string[] PlantSheetNames =
        { "peashooter", "sunflower", "snowpea", "repeater" };

    private static readonly string[] SeedPacketAssetNames =
        { "peashooter_seedpacket", "sunflower_seedpacket", "snowpea_seedpacket", "repeater_seedpacket" };

    private readonly Texture2D _background;
    private readonly Texture2D _pixel;
    private readonly SeedSlot _seedSlot;
    private readonly Texture2D _shovelTexture;
    private readonly Rectangle _shovelBounds;
    private readonly Texture2D[] _plantSheets;
    private readonly Rectangle _plotBounds;

    private IPlant _currentPlant;

    private const int ScreenWidth = 800;
    private const int ScreenHeight = 600;
    private const int PlotX = 350;
    private const int PlotY = 300;
    private const int PlotSize = 80;
    private const int TrayX = 10;
    private const int TrayY = 8;
    private const int TrayWidth = 446;
    private const int ShovelSize = 70;
    private const int SpriteSheetCols = 12;
    private const int SpriteSheetRows = 2;
    private const int FrameWidth = 80;
    private const int FrameHeight = 80;
    private const float FrameTime = 0.05f;

    public int SelectedPlantType { get; private set; } = -1;

    public Map(ContentManager content, GraphicsDevice device)
    {
        _pixel = new Texture2D(device, 1, 1);
        _pixel.SetData(new[] { Color.White });

        _background = content.Load<Texture2D>("day_level");
        _shovelTexture = content.Load<Texture2D>("shovel");

        var trayTexture = content.Load<Texture2D>("seedslot");
        var packetTextures = new Texture2D[PlantSheetNames.Length];
        for (int i = 0; i < PlantSheetNames.Length; i++)
            packetTextures[i] = content.Load<Texture2D>(SeedPacketAssetNames[i]);

        _seedSlot = new SeedSlot(trayTexture, packetTextures, _pixel, TrayX, TrayY);

        int shovelX = TrayX + TrayWidth + 8;
        int shovelY = TrayY + (_seedSlot.TrayBounds.Height - ShovelSize) / 2;
        _shovelBounds = new Rectangle(shovelX, shovelY, ShovelSize, ShovelSize);

        _plantSheets = new Texture2D[PlantSheetNames.Length];
        for (int i = 0; i < PlantSheetNames.Length; i++)
            _plantSheets[i] = content.Load<Texture2D>(PlantSheetNames[i]);

        _plotBounds = new Rectangle(PlotX, PlotY, PlotSize, PlotSize);
    }

    public void SelectPlant(int plantType)
    {
        SelectedPlantType = plantType;
        _currentPlant = CreatePlant(plantType, PlotX, PlotY);
    }

    public void ClearPlant()
    {
        SelectedPlantType = -1;
        _currentPlant = null;
    }

    public int GetSeedPacketIndexAt(SysPoint screenPos)
    {
        return _seedSlot.HitTestPacket(screenPos.X, screenPos.Y);
    }

    public bool IsShovelAt(SysPoint screenPos)
    {
        return _shovelBounds.Contains(screenPos.X, screenPos.Y);
    }

    public void Update(GameTime gameTime)
    {
        _currentPlant?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawBackground(spriteBatch);

        _currentPlant?.Draw(spriteBatch);

        _seedSlot.Draw(spriteBatch, SelectedPlantType);

        spriteBatch.Draw(_shovelTexture, _shovelBounds, Color.White);
    }

    private IPlant CreatePlant(int typeIndex, float x, float y)
    {
        if (typeIndex < 0 || typeIndex >= _plantSheets.Length)
            return null;

        var anim = CreateSpriteSheetAnimation(_plantSheets[typeIndex]);
        var sprite = new AnimatedSprite(anim, new Vector2(x, y));

        return typeIndex switch
        {
            0 => new Peashooter(sprite, x, y),
            1 => new Sunflower(sprite, x, y),
            2 => new SnowPea(sprite, x, y),
            3 => new Repeater(sprite, x, y),
            _ => null
        };
    }

    private void DrawBackground(SpriteBatch spriteBatch)
    {
        var sourceRect = new Rectangle((int)(_background.Width * 0.15f), 0, (int)(_background.Width * 0.58f), _background.Height);
        spriteBatch.Draw(_background, new Rectangle(0, 0, ScreenWidth, ScreenHeight), sourceRect, Color.White);
    }

    private static Animation CreateSpriteSheetAnimation(Texture2D sheet)
    {
        var frames = new List<ITextureRegion>();
        for (int r = 0; r < SpriteSheetRows; r++)
            for (int c = 0; c < SpriteSheetCols; c++)
                frames.Add(new TextureRegion(
                    $"frame_{r * SpriteSheetCols + c}",
                    sheet,
                    new Rectangle(c * FrameWidth, r * FrameHeight, FrameWidth, FrameHeight)));
        return new Animation(frames, FrameTime);
    }
}
