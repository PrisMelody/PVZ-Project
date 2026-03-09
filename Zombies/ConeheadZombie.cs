using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ConeheadZombie : HatZombie
{


    public ConeheadZombie(ITextureRegion coneRegion, float coneScale,
        ITextureRegion baseRegion, float baseScale, float x, float y, int lane)
    {
        _region = coneRegion;
        _scale = coneScale;
        _wrappedZombie = new BasicZombie(baseRegion, baseScale, x, y, lane);
        Health = 640;
    }
}
