using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Projectile : IProjectile
{
    public int Damage { get; protected set; }
    public float Speed { get; protected set; }
    public float XPos { get; set; }
    public float YPos { get; set; }
    public int DrawOrder { get; set; }

    protected Projectile(float x, float y, int damage, float speed)
    {
        XPos = x;
        YPos = y;
        Damage = damage;
        Speed = speed;
        DrawOrder = 30;
    }

    public virtual void Move()
    {
        XPos += Speed;
    }

    public virtual void Update(GameTime gameTime)
    {
        Move();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        // TODO: Draw projectile sprite
    }
}
