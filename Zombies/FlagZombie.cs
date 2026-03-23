using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FlagZombie : BasicZombie
{
    public FlagZombie(ITextureRegion region, float scale, float x, float y)
        : base(region, scale, x, y)
    {
        Speed = 0.6f;
    }
}