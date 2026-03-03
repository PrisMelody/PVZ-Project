using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprites;

public abstract class Plant : IPlant
{
    protected AnimatedSprite _sprite;

    public int Health { get; set; }
    public bool IsDead { get; set; }
    public float XPos { get; set; }
    public float YPos { get; set; }
    public int DrawOrder { get; set; }

    protected Plant(AnimatedSprite sprite, float x, float y, int health)
    {
        _sprite = sprite;
        XPos = x;
        YPos = y;
        Health = health;
        DrawOrder = 20;
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            IsDead = true;
        }
    }

    public virtual void Update(GameTime gameTime)
    {
        _sprite?.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        _sprite?.Draw(spriteBatch);
    }
}
