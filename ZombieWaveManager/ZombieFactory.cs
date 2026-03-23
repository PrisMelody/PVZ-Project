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

    // Initialize zombie factory with sprites for all types of zombies. Should be done at beginning of the level.
    public ZombieFactory(
        ITextureRegion basicZombieTexture,
        ITextureRegion coneheadTexture,
        ITextureRegion bucketheadTexture,
        ITextureRegion flagTexture)
    {
        _basicZombieTexture = basicZombieTexture;
        _coneheadTexture = coneheadTexture;
        _bucketheadTexture = bucketheadTexture;
        _flagTexture = flagTexture;
    }

    // Since not all zombie have decor, so decorScale is an optional parameter with default value to make it optional.
    public IZombie CreateZombie(ZombieType type, int lane, float scale, float decorScale = 1.0f)
    {
        // Coordinate for spawning the zombie.
        int x = 0, y = 0;
        // Calculate coordinate based on the lane.
        switch (lane)
        {
            // TODO: add coordinate for each lane.
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
        switch (type)
        {
            case ZombieType.Basic:
                return new BasicZombie(_basicZombieTexture, scale, x, y);

            case ZombieType.Conehead:
                return new ConeheadZombie(_basicZombieTexture, decorScale, _coneheadTexture, scale, x, y);

            case ZombieType.Buckethead:
                return new BucketheadZombie(_basicZombieTexture, decorScale, _bucketheadTexture, scale, x, y);
            
            case ZombieType.Flag:
                return new FlagZombie(_basicZombieTexture, scale, x, y);

            default:
                throw new ArgumentOutOfRangeException(nameof(type), $"Unknown zombie type: {type}");
        }
    }
}