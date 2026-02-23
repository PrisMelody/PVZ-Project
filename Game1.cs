using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PVZ_Project;

public class Game1 : Game, IGameInputHandler, IPlayerActions
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private IController _mouseController;

    private IMap _map;
    private IZombie [] testZombies;

    public int SelectedPlantType => _map?.SelectedPlantType ?? -1;

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

        _map = new Map(Content, GraphicsDevice);

        testZombies = new IZombie[4];
        testZombies[0] = new BasicZombie(900.0f, 90.0f);
        testZombies[1] = new ConeheadZombie(900.0f, 150.0f);
        testZombies[2] = new BucketheadZombie(900.0f, 375.0f);
        testZombies[3] = new FlagZombie(900.0f, 475.0f);


        TempZombieSpriteHandler.Zombies = Content.Load<Texture2D>("images/base_zombiesforproj");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _mouseController?.Update();
        _map?.Update(gameTime);
        foreach (IZombie zombie in testZombies)
        {
            zombie.Update(gameTime);
        }

        base.Update(gameTime);
    }

    public void HandleClick(Point screenPosition)
    {
        var pt = new System.Drawing.Point(screenPosition.X, screenPosition.Y);

        if (_map.IsShovelAt(pt))
        {
            ClearPlant();
            return;
        }

        int seedIndex = _map.GetSeedPacketIndexAt(pt);
        if (seedIndex >= 0)
        {
            new SelectPlantCommand(this, seedIndex).Execute();
            return;
        }
    }

    public void SetSelectedPlant(int plantType)
    {
        _map.SelectPlant(plantType);
    }

    public void PlacePlant(Point gridOrScreenPosition)
    {
        // Full game: place on grid. Not used this sprint.
    }

    public void ClearPlant()
    {
        _map.ClearPlant();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _map.Draw(_spriteBatch);
        foreach (IZombie zombie in testZombies)
        {
            zombie.Draw(_spriteBatch);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
