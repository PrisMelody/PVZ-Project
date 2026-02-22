using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PVZ_Project;

public class Game1 : Game, IGameInputHandler, IPlayerActions
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private IController _mouseController;

    private IZombie testZombie;

    /// <summary>Currently displayed plant type on the single plot. -1 = no plant (shovel cleared).</summary>
    public int SelectedPlantType { get; private set; } = -1;

    private Rectangle _singlePlotBounds;
    private Rectangle[] _seedPacketBounds;
    private Rectangle _shovelBounds;
    private Texture2D _pixel;
    private Texture2D _plantPlaceholder;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.ApplyChanges();

        _mouseController = new MouseController(this);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });

        _singlePlotBounds = new Rectangle(200, 200, 80, 80);

        _seedPacketBounds = new[]
        {
            new Rectangle(80, 500, 70, 80),   // type 0
            new Rectangle(160, 500, 70, 80),  // type 1
            new Rectangle(240, 500, 70, 80),  // type 2
        };

        _shovelBounds = new Rectangle(700, 500, 70, 80);

        try
        {
            _plantPlaceholder = Content.Load<Texture2D>("images/Placeholder Plant");
        }
        catch
        {
            _plantPlaceholder = null;
        }

        testZombie = new BasicZombie(500.0f, 100.0f);
        TempZombieSpriteHandler.BasicZombie = Content.Load<Texture2D>("images/Placeholder Zombie");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _mouseController?.Update();
        testZombie.Update(gameTime);

        base.Update(gameTime);
    }

    public void HandleClick(Point screenPosition)
    {
        if (IsShovelArea(screenPosition))
        {
            ClearPlant();
            return;
        }
        if (IsInPlantMenu(screenPosition))
        {
            int type = GetPlantType(screenPosition);
            new SelectPlantCommand(this, type).Execute();
        }
    }

    public void SetSelectedPlant(int plantType)
    {
        SelectedPlantType = plantType;
    }

    public void PlacePlant(Point gridOrScreenPosition)
    {
        // Full game: place on grid. Not used this sprint.
    }

    public void ClearPlant()
    {
        SelectedPlantType = -1;
    }

    private bool IsInPlantMenu(Point screenPosition)
    {
        for (int i = 0; i < _seedPacketBounds.Length; i++)
        {
            if (_seedPacketBounds[i].Contains(screenPosition))
                return true;
        }
        return false;
    }

    private int GetPlantType(Point screenPosition)
    {
        for (int i = 0; i < _seedPacketBounds.Length; i++)
        {
            if (_seedPacketBounds[i].Contains(screenPosition))
                return i;
        }
        return -1;
    }

    private bool IsShovelArea(Point screenPosition)
    {
        return _shovelBounds.Contains(screenPosition);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_pixel, _singlePlotBounds, new Color(34, 139, 34));

        if (SelectedPlantType >= 0)
        {
            if (_plantPlaceholder != null)
                _spriteBatch.Draw(_plantPlaceholder, _singlePlotBounds, Color.White);
            else
                _spriteBatch.Draw(_pixel, _singlePlotBounds, new Color(255, 200, 100));
        }

        testZombie.Draw(_spriteBatch);

        for (int i = 0; i < _seedPacketBounds.Length; i++)
            _spriteBatch.Draw(_pixel, _seedPacketBounds[i], new Color(200, 180, 80));

        _spriteBatch.Draw(_pixel, _shovelBounds, new Color(139, 90, 43));

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
