using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Projectile : IProjectile
{
    public int Damage { get; protected set; }
    public float Speed { get; protected set; }
    public float XPos { get; set; }
    public float YPos { get; set; }
    public int DrawOrder { get; set; }
    public bool IsDead { get; private set; }
    private float _maxDistance = 1000;
    private float _startX;

    protected Texture2D _texture;

    protected Projectile(float x, float y, int damage, float speed, Texture2D texture)    {
        XPos = x;
        YPos = y;
        Damage = damage;
        Speed = speed;
        DrawOrder = 30;
        _texture = texture;
        _startX = x;    
        
    }

    public virtual void Move()
    {
        XPos += Speed;
    }

    public virtual void Update(GameTime gameTime)
    {
        Move();
        if (XPos - _startX > _maxDistance)
        {
            IsDead = true;
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (_texture == null) return;

        spriteBatch.Draw(
            _texture,
            new Vector2(XPos, YPos),
            Color.White
        );
        // TODO: Draw projectile sprite
    }
}
