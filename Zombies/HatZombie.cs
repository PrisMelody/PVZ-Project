using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Abstract class used for the Buckethead and Conehead Zombies.
//Could also be used to implement other zombies (even without hats!), like the screen door shield zombie, in future sprints.

public abstract class HatZombie : IZombie
{
    protected IZombie _wrappedZombie;
    protected ITextureRegion _region;
    protected float _scale;
    protected int _hatFallOffThreshold;
    
    public int SpawnWaveIndex
    {
        get => _wrappedZombie.SpawnWaveIndex;
        set => _wrappedZombie.SpawnWaveIndex = value;
    }

    public float MaxRange
    {
        get => _wrappedZombie.MaxRange;
    }

     public float MinRange
    {
        get => _wrappedZombie.MinRange;
    }

    public float Speed
    {
        get => _wrappedZombie.Speed;
        set => _wrappedZombie.Speed = value;
    }
    public float xCoord
    {
        get => _wrappedZombie.xCoord;
        set => _wrappedZombie.xCoord = value;
    }
    public float yCoord
    {
        get => _wrappedZombie.yCoord;
        set => _wrappedZombie.yCoord = value;
    }
    public int Health
    {
        get => _wrappedZombie.Health;
        set => _wrappedZombie.Health = value;
    }
    public bool IsDead
    {
        get => _wrappedZombie.IsDead;
        set => _wrappedZombie.IsDead = value;
    }
    public bool IsAttacking
    {
        get => _wrappedZombie.IsAttacking;
        set => _wrappedZombie.IsAttacking = value;
    }
    public int DrawOrder
    {
        get => _wrappedZombie.DrawOrder;
        set => _wrappedZombie.DrawOrder = value;
    }

    public int Lane
    {
        get => _wrappedZombie.Lane;
    }

    public void Move() => _wrappedZombie.Move();
    public void Attack() => _wrappedZombie.Attack();

    public void TakeDamage(int amount)
    {
        _wrappedZombie.TakeDamage(amount);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Health <= _hatFallOffThreshold)
        {
            _wrappedZombie.Draw(spriteBatch); //TODO: currently, this doesn't work as intended, as it just uses the sprite for the "outer" zombie still.
        }
        else
        {
            spriteBatch.Draw(
                _region.Texture,
                // Since hat change the height of zombies, so I added 30.0f to correct the shift of position.
                new Vector2(xCoord, yCoord - 30.0f),
                _region.SourceRectangle,
                Color.White,
                0.0f,
                Vector2.Zero,
                _scale,
                SpriteEffects.None,
                0.0f
            );
        }
    }

    public void Update(GameTime gameTime) => _wrappedZombie.Update(gameTime);
}
