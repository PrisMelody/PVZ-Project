using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
/*
* This class is like a dispatch center for zombies.
* It stores all sprites for all types of zombies,
* and process all request to spawn zombies by
* calling designated constructor for each type of zombie.
*/
// TODO Should replace the x, y coordinate with a constant from a config file.
public class ZombieFactory
{
    // This part is for ZombieFactory to store all sprites of all types of zombies.
    private readonly ITextureRegion _basicZombieTexture;
    private readonly ITextureRegion _coneheadTexture;
    private readonly ITextureRegion _bucketheadTexture;
    private readonly ITextureRegion _flagTexture;

    private readonly ITextureRegion _jetpackTexture;

    private float _scale;

    // Initialize zombie factory with sprites for all types of zombies. Should be done at beginning of the level.
    public ZombieFactory(
        ITextureRegion basicZombieTexture,
        ITextureRegion coneheadTexture,
        ITextureRegion bucketheadTexture,
        ITextureRegion flagTexture,
        ITextureRegion jetpackTexture,
        float scale)
    {
        _basicZombieTexture = basicZombieTexture;
        _coneheadTexture = coneheadTexture;
        _bucketheadTexture = bucketheadTexture;
        _flagTexture = flagTexture;
        _jetpackTexture = jetpackTexture;
        _scale = scale;
    }

    // Since not all zombie have decor, so decorScale is an optional parameter with default value to make it optional.
    public IZombie CreateZombie(ZombieType type, int lane, float decorScale = 1.0f)
    {
        switch (type)
        {
            case ZombieType.Basic:
                return new BasicZombie(_basicZombieTexture, _scale, lane);

            case ZombieType.Conehead:
                return new ConeheadZombie(_coneheadTexture, decorScale, _basicZombieTexture, _scale, lane);

            case ZombieType.Buckethead:
                return new BucketheadZombie(_bucketheadTexture, decorScale, _basicZombieTexture, _scale, lane);
            
            case ZombieType.Flag:
                return new FlagZombie(_flagTexture, _scale, lane);

            case ZombieType.Jetpack:
                return new JetpackZombie(_jetpackTexture, _scale * 0.15f, lane);

            default:
                throw new ArgumentOutOfRangeException(nameof(type), $"Unknown zombie type: {type}");
        }
    }
}