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
        zombie.DrawColor = Color.LightSkyBlue; //Placeholder to make sure these actually have a unique effect.
        //TODO: make something that actually freezes zombies. I tried making a "hat" that could do it, but was unable to get it to apply properly.
        //Said hat is in the Zombies folder if someone else wants to take a crack at it.
        IsDead = true;
    }
}
