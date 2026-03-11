using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// TODO Need to implement ZombieFactory.
public class ZombieFactory
{
    private readonly Texture2D _basicZombieTexture;
    private readonly Texture2D _coneheadZombieTexture;
    private readonly Texture2D _bucketheadZombieTexture;

    public ZombieFactory(
        Texture2D basicZombieTexture,
        Texture2D coneheadZombieTexture,
        Texture2D bucketheadZombieTexture)
    {
        _basicZombieTexture = basicZombieTexture;
        _coneheadZombieTexture = coneheadZombieTexture;
        _bucketheadZombieTexture = bucketheadZombieTexture;
    }

    public IZombie CreateZombie(ZombieType type, int lane)
    {
        switch (type)
        {
            case ZombieType.Basic:
                return new BasicZombie(_basicZombieTexture, spawnPosition);

            case ZombieType.Conehead:
                return new ConeheadZombie(_coneheadZombieTexture, spawnPosition);

            case ZombieType.Buckethead:
                return new BucketheadZombie(_bucketheadZombieTexture, spawnPosition);

            default:
                throw new ArgumentOutOfRangeException(nameof(type), $"Unknown zombie type: {type}");
        }
    }
}