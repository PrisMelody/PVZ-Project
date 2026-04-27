using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprites;
using System;
using System.IO;

namespace PVZ_Project;

public class Game1 : Game, IGameInputHandler, IPlayerActions
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private IController _mouseController;

    private Map _map;
    private ZombieFactory _zombieFactory;
    private ZombieSpawnManager _zombieSpawnManager;
    private ZombieManager _zombieManager;

    private CollisionManager _collisionManager;
    private CollectableManager _collectableManager;
    private IGameState _gameState;
    private bool _shovelActive;

    // Reused across restarts (textures are cached by ContentManager)
    private TextureRegion _basicRegion;
    private TextureRegion _flagRegion;
    private TextureRegion _coneRegion;
    private TextureRegion _bucketRegion;
    private TextureRegion _jetpackRegion;
    private Texture2D _sunTexture;
    private Texture2D _coinTexture;

    private EndScreen _endScreen;
    private KeyboardState _prevKeyboard;

    // Default scale for sprite. Should be configurable in menu or command.
    private float scale = 1.0f;

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
        SoundManager.LoadContent(Content);
        SoundManager.PlayMusic();

        // Load textures that are reused on restart (ContentManager caches them)
        var zombieSheet = Content.Load<Texture2D>("images/base_zombiesforproj");
        _basicRegion  = new TextureRegion("basic",   zombieSheet, new Rectangle(475, 42,  86, 153));
        _flagRegion   = new TextureRegion("flag",    zombieSheet, new Rectangle(624, 40, 102, 152));
        _coneRegion   = new TextureRegion("cone",    zombieSheet, new Rectangle( 28, 10,  86, 311));
        _bucketRegion = new TextureRegion("bucket",  zombieSheet, new Rectangle(238, 16,  96, 179));
        _jetpackRegion = new TextureRegion("jetpack",
            Content.Load<Texture2D>("images/DiscoJetpack"), new Rectangle(0, 0, 686, 969));

        _sunTexture  = Content.Load<Texture2D>("sun");
        _coinTexture = Content.Load<Texture2D>("coin");

        // EndScreen needs a 1x1 pixel and the shared font
        var pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White });
        var font = Content.Load<SpriteFont>("DefaultFont");
        _endScreen = new EndScreen(pixel, font);

        StartNewGame();
    }

    // Creates / recreates all gameplay objects. Safe to call for restart.
    private void StartNewGame()
    {
        _shovelActive = false;
        _gameState    = new GameState();
        _map          = new Map(Content, GraphicsDevice);

        _zombieFactory       = new ZombieFactory(_basicRegion, _coneRegion, _bucketRegion,
                                                  _flagRegion, _jetpackRegion, scale);
        _collectableManager  = new CollectableManager(_sunTexture, _coinTexture);
        _zombieManager       = new ZombieManager(_collectableManager);
        _collisionManager    = new CollisionManager(_zombieManager, _map);

        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                   "ZombieWaveManager", "example.xml");
        LevelSpawnData levelData = LevelLoader.LoadFromXml(path);
        _zombieSpawnManager = new ZombieSpawnManager(levelData, _zombieManager, _zombieFactory);

        // Wire victory: all waves cleared + no zombies alive
        _zombieSpawnManager.OnLevelCompleted += () => _gameState.SetStatus(GameStatus.Won);
    }

    protected override void Update(GameTime gameTime)
    {
        var keyboard = Keyboard.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            keyboard.IsKeyDown(Keys.Escape))
            Exit();

        // When the game has ended, only listen for R (restart)
        if (_gameState.Status == GameStatus.Won || _gameState.Status == GameStatus.Lost)
        {
            if (keyboard.IsKeyDown(Keys.R) && !_prevKeyboard.IsKeyDown(Keys.R))
                StartNewGame();

            _prevKeyboard = keyboard;
            base.Update(gameTime);
            return;
        }

        _mouseController?.Update();
        _map?.Update(gameTime);
        _zombieSpawnManager?.Update(gameTime);

        for (int i = 0; i <= 4; i++)
            _collisionManager.CheckZombiePlantCollision(i);

        _zombieManager?.Update(gameTime);
        _collectableManager?.Update(gameTime);

        _collisionManager.CheckProjectileZombieCollision();
        _collisionManager.CheckSplashZombieCollision();

        // Defeat: any zombie walked past x=0 (its lane's lawn mower is already gone)
        if (_zombieManager?.HasZombieReachedHome() == true)
            _gameState.SetStatus(GameStatus.Lost);

        _prevKeyboard = keyboard;
        base.Update(gameTime);
    }

    public void HandleClick(Point screenPosition)
    {
        // Ignore clicks during end screen
        if (_gameState.Status != GameStatus.Playing)
            return;

        if (_collectableManager != null)
        {
            var pickup = _collectableManager.TryCollectAt(screenPosition);
            if (pickup.Collected)
            {
                if (pickup.Kind == CollectableKind.Sun)
                    _gameState.AddSun(pickup.Value);
                else
                    _gameState.AddCoins(pickup.Value);
                return;
            }
        }

        if (_map.IsShovelAt(screenPosition))
        {
            _shovelActive = !_shovelActive;
            if (_shovelActive)
                _map.ClearPlant();
            return;
        }

        int seedIndex = _map.GetSeedPacketIndexAt(screenPosition);
        if (seedIndex >= 0)
        {
            _shovelActive = false;
            var plantType = _map.GetPlantTypeForSlot(seedIndex);
            if (plantType.HasValue && _gameState.Sun >= _map.GetCostForSlot(seedIndex))
                new SelectPlantCommand(this, plantType.Value).Execute();
            return;
        }

        if (_shovelActive)
        {
            UseShovel(screenPosition);
            return;
        }

        if (_map.SelectedPlantType.HasValue)
            PlacePlant(screenPosition);
    }

    public void SetSelectedPlant(PlantType type)
    {
        _map.SelectPlant(type);
    }

    public void PlacePlant(Point screenPosition)
    {
        int cost = _map.GetCostForSelectedPlant();
        if (_gameState.Sun >= cost && _map.TryPlacePlantAt(screenPosition))
        {
            _gameState.SpendSun(cost);
            _map.ClearPlant();
        }
    }

    public void ClearPlant()
    {
        _map.ClearPlant();
    }

    public void UseShovel(Point screenPosition)
    {
        _map.TryRemovePlantAt(screenPosition);
        _shovelActive = false;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _map.Draw(_spriteBatch, _gameState.Sun);
        _zombieManager?.Draw(_spriteBatch);
        _collectableManager?.Draw(_spriteBatch);

        // Draw end overlay on top of everything when game has ended
        var status = _gameState.Status;
        if (status == GameStatus.Won || status == GameStatus.Lost)
            _endScreen.Draw(_spriteBatch, status);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
