using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class LawnMower : Projectile
{
    private readonly static float xSpriteOffset = -55f;
    private readonly static float ySpriteOffset = -20f;

    public LawnMower(float x, float y,Texture2D _texture)
        : base(x, y, 10000, 0f,_texture)
    {
    }

    public override void OnHit(IZombie zombie)
    {
        zombie.TakeDamage(Damage);
        if (Speed == 0)
        {
            //TODO: add sounds here at some point.
            Speed = 3f;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (_texture == null) return;

        spriteBatch.Draw(
            _texture,
            new Vector2(XPos + xSpriteOffset, YPos + ySpriteOffset),
            Color.White
        );
        // TODO: Draw projectile sprite
    }
}
