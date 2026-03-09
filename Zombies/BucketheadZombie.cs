using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BucketheadZombie : HatZombie
{
    
    public BucketheadZombie(ITextureRegion bucketRegion, float bucketScale,
        ITextureRegion baseRegion, float baseScale, float x, float y, int lane)
    {
        _region = bucketRegion;
        _scale = bucketScale;
        _wrappedZombie = new BasicZombie(baseRegion, baseScale, x, y, lane);
        Health = 1370;
        _hatFallOffThreshold = 270;
    }
}
