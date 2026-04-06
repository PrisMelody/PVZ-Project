using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class FrozenPea : Projectile
{
    public FrozenPea(float x, float y,Texture2D _texture)
        : base(x, y, 20, 3f, _texture)
    {
    }

    public override void OnHit(IZombie zombie)
    {
        zombie.TakeDamage(Damage);
        //TODO: Add the frozen effect to the zombie, presumably in the form of some kind of wrapper.
        //System.Console.WriteLine("Freeze!");
        IsDead = true;
    }
}
