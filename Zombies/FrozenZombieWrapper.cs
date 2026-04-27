using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//This was a failed attempt at creating a "hat" that would freeze a zombie.
//I haven't been able to get it to apply properly.
public class FrozenZombieWrapper : HatZombie
{

    static bool _evenFrame = false;

    public FrozenZombieWrapper(IZombie zombie)
    {
        _wrappedZombie = zombie;
        _wrappedZombie.DrawColor = Color.SkyBlue;
    }

    public override void Update(GameTime gameTime) //Makes the zombie only update every other frame.
    {
        if (_evenFrame)
        base.Update(gameTime);
        _evenFrame = !_evenFrame;
    }

}
