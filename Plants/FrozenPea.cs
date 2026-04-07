using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class FrozenPea : Projectile
{
    public FrozenPea(float x, float y,Texture2D _texture)
        : base(x, y, 20, 3f, _texture)
    {
    }
}
