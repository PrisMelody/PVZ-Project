using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PVZ_Project;

// Add this outside or inside your namespace
public enum GameState
{
    StartCutscenePanRight,
    StartCutscenePanLeft,
    Playing,
    DeathCutscene
}

public class Game1 : Game, IGameInputHandler, IPlayerActions
{
    // ... your existing variables ...
    
    // --- NEW CAMERA VARIABLES ---
    private Camera _camera;
    private GameState _currentState;
    private float _panSpeed = 400f; // How fast the camera moves (pixels per second)
    private float _maxPanX = 800f;  // How far right to look before stopping

    public Game1()
    {
        // ... existing constructor ...
    }

    protected override void Initialize()
    {
        // ... existing initialize code ...
        
        _camera = new Camera();
        _currentState = GameState.StartCutscenePanRight; // Start the game in the cutscene
        
        base.Initialize();
    }
    // ...

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

    float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

    switch (_currentState)
    {
        case GameState.StartCutscenePanRight:
            _camera.Position += new Vector2(_panSpeed * deltaTime, 0);
            
            // Stop panning when we reach the right side of the yard
            if (_camera.Position.X >= _maxPanX)
            {
                _camera.Position = new Vector2(_maxPanX, _camera.Position.Y);
                _currentState = GameState.StartCutscenePanLeft; // Switch direction
            }
            break;

        case GameState.StartCutscenePanLeft:
            _camera.Position -= new Vector2(_panSpeed * deltaTime, 0);
            
            // Stop panning when we get back to the start
            if (_camera.Position.X <= 0)
            {
                _camera.Position = Vector2.Zero;
                _currentState = GameState.Playing; // Start the actual game!
            }
            break;

        case GameState.Playing:
            // ONLY update game logic and mouse when playing
            _mouseController?.Update();
            _map?.Update(gameTime);
            foreach (IZombie zombie in testZombies)
            {
                zombie.Update(gameTime);
                
                // Example check for death cutscene: 
                // If a zombie reaches the house (e.g., X < 0)
                // if (zombie.Position.X < 0) { _currentState = GameState.DeathCutscene; }
            }
            break;

        case GameState.DeathCutscene:
            // Pan right or left depending on where the house is to show the zombie entering
            _camera.Position -= new Vector2(_panSpeed * deltaTime, 0);
            break;
    }

    base.Update(gameTime);
}
    public void HandleClick(Point screenPosition)
    {
        // 1. Convert the physical screen click into the camera's world space
        Vector2 worldPos = _camera.ScreenToWorldSpace(new Vector2(screenPosition.X, screenPosition.Y));
    
        // 2. Create the point using the WORLD coordinates, not the screen coordinates
        var pt = new System.Drawing.Point((int)worldPos.X, (int)worldPos.Y);

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

    // Pass the camera's view matrix here!
    // This tells MonoGame to offset all drawn textures by the camera's position.
    _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
    
    _map.Draw(_spriteBatch);
    foreach (IZombie zombie in testZombies)
    {
        zombie.Draw(_spriteBatch);
    }
    
    _spriteBatch.End();

    base.Draw(gameTime);
    }
}
