using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BucketheadZombie : IZombie
{
    private readonly IZombie _wrappedZombie;
    private readonly ITextureRegion _bucketRegion;
    private readonly float _bucketScale;

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
    public int SpawnWaveIndex { get; set; }

    public BucketheadZombie(ITextureRegion bucketRegion, float bucketScale,
        ITextureRegion baseRegion, float baseScale, float x, float y)
    {
        _bucketRegion = bucketRegion;
        _bucketScale = bucketScale;
        _wrappedZombie = new BasicZombie(baseRegion, baseScale, x, y);
        Health = 1370;
    }

    public void Move() => _wrappedZombie.Move();
    public void Attack() => _wrappedZombie.Attack();

    public void TakeDamage(int amount)
    {
        _wrappedZombie.TakeDamage(amount);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Health <= 270)
        {
            _wrappedZombie.Draw(spriteBatch);
        }
        else
        {
            spriteBatch.Draw(
                _bucketRegion.Texture,
                new Vector2(xCoord, yCoord),
                _bucketRegion.SourceRectangle,
                Color.White,
                0.0f,
                Vector2.Zero,
                _bucketScale,
                SpriteEffects.None,
                0.0f
            );
        }
    }

    public void Update(GameTime gameTime) => _wrappedZombie.Update(gameTime);
}
