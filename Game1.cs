using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprites;

namespace PVZ_Project;

public class Game1 : Game, IGameInputHandler, IPlayerActions
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private IController _mouseController;

    private Map _map;
    private ZombieManager _zombieManager;
    private IGameState _gameState;
    private bool _shovelActive;

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

        var plantFactory = new PlantFactory();
        plantFactory.LoadContent(Content);

        _gameState = new GameState();
        _map = new Map(Content, GraphicsDevice, plantFactory);

        //Right now, the zombies are using placeholder non-animated sprites. This will be removed once they have animated sprites like the plants.
        var zombieSheet = Content.Load<Texture2D>("images/base_zombiesforproj");
        var basicRegion = new TextureRegion("basic", zombieSheet, new Rectangle(475, 42, 86, 153));
        var flagRegion = new TextureRegion("flag", zombieSheet, new Rectangle(624, 40, 102, 152));
        var coneRegion = new TextureRegion("cone", zombieSheet, new Rectangle(28, 10, 86, 311));
        var bucketRegion = new TextureRegion("bucket", zombieSheet, new Rectangle(238, 16, 96, 179));

        _zombieManager = new ZombieManager();
        _zombieManager.Add(new BasicZombie(basicRegion, 0.5f, 900.0f, 90.0f));
        _zombieManager.Add(new ConeheadZombie(coneRegion, 0.6f, basicRegion, 0.5f, 900.0f, 150.0f));
        _zombieManager.Add(new BucketheadZombie(bucketRegion, 0.5f, basicRegion, 0.5f, 900.0f, 375.0f));
        _zombieManager.Add(new FlagZombie(flagRegion, 0.5f, 900.0f, 475.0f));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _mouseController?.Update();
        _map?.Update(gameTime);
        _zombieManager?.Update(gameTime);

        //TEMPORARY COLLISION CHECKER: THIS IS DUMB AND BAD, REPLACE IT

//TODO: Currently, this is partially working but it's very janky. Fix the DetectionBox system and expand to check all the plots.
        foreach(IZombie zombie in _zombieManager.Zombies){
            for(int i = 1; i < 4; i++)
            {
                IGridPlot currentPlot = _map._grid.GetPlot(1, i);
                if(!currentPlot.IsOccupied)
                {
                    continue;
                }
                if (zombie.PlantDetectionBox.Contains(currentPlot.Plant.XPos, currentPlot.Plant.YPos))
                {
                    zombie.IsAttacking = true;
                    System.Console.WriteLine("Collision detected");
                }
            }
            
        }

        base.Update(gameTime);
    }

    public void HandleClick(Point screenPosition)
    {
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
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
