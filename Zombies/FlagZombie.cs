using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FlagZombie : BasicZombie
{
    public FlagZombie(ITextureRegion region, float scale, float x, float y, int lane)
        : base(region, scale, x, y, lane)
    {
        Speed = 0.6f;
    }
}
