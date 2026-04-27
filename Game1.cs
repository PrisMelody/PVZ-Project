using System.Reflection.Metadata.Ecma335;
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
        

        _gameState = new GameState();
        _map = new Map(Content, GraphicsDevice);

        /*
        * Right now, the zombies are using placeholder non-animated sprites. This will be removed once they have animated sprites like the plants.
        * Load sprite sheet into ZombieFactory instance so it holds sprite for each types of zombie.
        */
        var zombieSheet = Content.Load<Texture2D>("images/base_zombiesforproj");
        var basicRegion = new TextureRegion("basic", zombieSheet, new Rectangle(475, 42, 86, 153));
        var flagRegion = new TextureRegion("flag", zombieSheet, new Rectangle(624, 40, 102, 152));
        var coneRegion = new TextureRegion("cone", zombieSheet, new Rectangle(28, 10, 86, 311));
        var bucketRegion = new TextureRegion("bucket", zombieSheet, new Rectangle(238, 16, 96, 179));
        _zombieFactory = new ZombieFactory(basicRegion, coneRegion, bucketRegion, flagRegion, scale);

        var sunTexture = Content.Load<Texture2D>("sun");
        var coinTexture = Content.Load<Texture2D>("coin");
        _collectableManager = new CollectableManager(sunTexture, coinTexture);

        // ZombieManager to manage active zombies.
        _zombieManager = new ZombieManager(_collectableManager);
        //CollisionManager to handle collisions. Go figure
        _collisionManager = new CollisionManager(_zombieManager, _map);


        /*
        * Load a level based on the file path.
        * TO-DO: Should develop a function to select path based on UI selection.
        */
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"ZombieWaveManager","example.xml");
        LevelSpawnData levelSpawnData = LevelLoader.LoadFromXml(path);
        // Initialize zombie dispatch center (ZombieSpawnManager instance).
        _zombieSpawnManager = new ZombieSpawnManager(levelSpawnData, _zombieManager, _zombieFactory);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _mouseController?.Update();
        _map?.Update(gameTime);
        _zombieSpawnManager?.Update(gameTime);

        for (int i = 0; i <= 4; i++) //TODO: remove magic numbers
        {
            _collisionManager.CheckZombiePlantCollision(i);
        }

        _zombieManager?.Update(gameTime);
        _collectableManager?.Update(gameTime);

        _collisionManager.CheckProjectileZombieCollision();
        _collisionManager.CheckSplashZombieCollision();
        base.Update(gameTime);
    }
        


    public void HandleClick(Point screenPosition)
    {
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
            if (plantType.HasValue)
                new SelectPlantCommand(this, plantType.Value).Execute();
            return;
        }

        if (_shovelActive)
        {
            UseShovel(screenPosition);
            return;
        }

        if (_map.SelectedPlantType.HasValue)
        {
            PlacePlant(screenPosition);
        }
    }

    public void SetSelectedPlant(PlantType type)
    {
        _map.SelectPlant(type);
    }

    public void PlacePlant(Point screenPosition)
    {
        if (_map.TryPlacePlantAt(screenPosition))
            _map.ClearPlant();
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
        _map.Draw(_spriteBatch);
        _zombieManager?.Draw(_spriteBatch);
        _collectableManager?.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

}
