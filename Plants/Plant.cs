using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprites;

public abstract class Plant : IPlant
{
    protected AnimatedSprite _sprite;

    protected Animation _idleAnim;
    protected Animation _actionAnim;
    protected Animation _currentAnim;

    public int Health { get; set; }
    public bool IsDead { get; set; }
    public float XPos { get; set; }
    public float YPos { get; set; }
    public int DrawOrder { get; set; }

    protected Plant(Animation idle, Animation action, float x, float y)
    {
        XPos = x;
        YPos = y;
        Health = 100; // Default health
        DrawOrder = 20;

        _idleAnim = idle;
        _actionAnim = action;
        _currentAnim = _idleAnim;

        _sprite = new AnimatedSprite(_idleAnim, new Vector2(x, y));
    }

   protected void PlayAnimation(Animation anim)
{
   _sprite.SetAnimation(anim);
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