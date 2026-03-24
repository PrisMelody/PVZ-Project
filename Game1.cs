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

        

        _gameState = new GameState();
        _map = new Map(Content, GraphicsDevice);

        //Right now, the zombies are using placeholder non-animated sprites. This will be removed once they have animated sprites like the plants.
        var zombieSheet = Content.Load<Texture2D>("images/base_zombiesforproj");
        var basicRegion = new TextureRegion("basic", zombieSheet, new Rectangle(475, 42, 86, 153));
        var flagRegion = new TextureRegion("flag", zombieSheet, new Rectangle(624, 40, 102, 152));
        var coneRegion = new TextureRegion("cone", zombieSheet, new Rectangle(28, 10, 86, 311));
        var bucketRegion = new TextureRegion("bucket", zombieSheet, new Rectangle(238, 16, 96, 179));

        _zombieManager = new ZombieManager();
        _zombieManager.Add(new BasicZombie(basicRegion, 0.5f, 0));
        _zombieManager.Add(new ConeheadZombie(coneRegion, 0.6f, basicRegion, 0.5f, 1));
        _zombieManager.Add(new BucketheadZombie(bucketRegion, 0.5f, basicRegion, 0.5f, 3));
        _zombieManager.Add(new FlagZombie(flagRegion, 0.5f, 4));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _mouseController?.Update();
        _map?.Update(gameTime);
        _zombieManager?.Update(gameTime);


        for (int i = 0; i <= 4; i++) //TODO: remove magic numbers
        {
            CheckZombiePlantCollision(i);
        }
        CheckProjectileZombieCollision();
        CheckSplashZombieCollision();
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

    private void CheckZombiePlantCollision(int lane) //TODO: stick this in its own class. 
    {
        foreach(IZombie zombie in _zombieManager.ZombiesByLane[lane])
        {
            foreach (IGridPlot currentGrid in _map._grid.Lanes[lane].Plots) //This is gross.
            {
                if (!currentGrid.IsOccupied){continue;}
                float distance = zombie.xCoord - currentGrid.Plant.XPos;
                if (distance < zombie.Range && distance > 0)
                {
                    zombie.IsAttacking = true;
                    currentGrid.Plant.TakeDamage(2); //TODO: replace with actual damage method.
                }
                break;
            }
        } 
    }

    private void CheckProjectileZombieCollision()
    {
        foreach (IProjectile projectile in _map.Projectiles)
        {
            int lane = Projectile.getLaneFromYPos[projectile.YPos];
            foreach(IZombie zombie in _zombieManager.ZombiesByLane[lane])
            {
                float distance = zombie.xCoord - projectile.XPos;
                if(distance < 0  && distance > -30)
                {
                    zombie.TakeDamage(projectile.Damage); //TODO: replace with actual projectile damage method.
                }
            }
        }
    }

    private void CheckSplashZombieCollision()
    {
        //TODO: once cherry bombs and mines are set up, add this.
        //This will probably end up being n^2, but given that explosions last for a single frame that's probably fine.
    }


}
