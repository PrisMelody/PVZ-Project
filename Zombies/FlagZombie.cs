using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FlagZombie : BasicZombie
{
    public FlagZombie(ITextureRegion region, float scale, int lane)
        : base(region, scale, lane)
    {
        Speed = 0.6f;
    }
}