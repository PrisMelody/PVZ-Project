using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BasicZombie : IZombie
{
    private readonly ITextureRegion _region;
    private readonly float _scale;

    public bool IsAttacking { get; set; } = false;
    public float Speed { get; set; } = 0.5f;
    public float xCoord { get; set; }
    public float yCoord { get; set; }
    public int Health { get; set; } = 270;
    public bool IsDead { get; set; }
    public int DrawOrder { get; set; }
    public Rectangle PlantDetectionBox{ get => new Rectangle((int) xCoord, (int) yCoord - 30, 300, 100);}
    //TODO: Making an entirely new rectangle may not be super efficient, but there doesn't seem to be a good way to change the x/y values on the fly.

    public BasicZombie(ITextureRegion region, float scale, float x, float y)
    {
        _region = region;
        _scale = scale;
        xCoord = x;
        yCoord = y;
    }

    public void Move()
    {
        xCoord -= Speed;
    }

    public void Attack()
    {
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            IsDead = true;
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _region.Texture,
            new Vector2(xCoord, yCoord),
            _region.SourceRectangle,
            Color.White,
            0.0f,
            Vector2.Zero,
            _scale,
            SpriteEffects.None,
            0.0f
        );
    }

    public void Update(GameTime gameTime)
    {
        if (!IsAttacking)
        {
            Move();
        }
        else
        {
            Attack();
            IsAttacking = false;
        }
    }
}
