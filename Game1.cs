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
    private IZombie testZombie;

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

        testZombie = new BasicZombie(500.0f, 100.0f);
        TempZombieSpriteHandler.BasicZombie = Content.Load<Texture2D>("images/Placeholder Zombie");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _mouseController?.Update();
        _map?.Update(gameTime);
        testZombie.Update(gameTime);

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
        testZombie.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
