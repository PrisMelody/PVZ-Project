using MonoGameLibrary.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
public class Peashooter : Plant

{
    private double _timer;

    public Peashooter(Animation idle, Animation action, float x, float y)
        : base(idle, action, x, y)
    {
        _timer = 0;
    }
    public override void Update(GameTime gameTime)
    {
        _timer += gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer > 5) // every 5 seconds
        {
            PlayAnimation(_actionAnim); // same animation for now
            System.Console.WriteLine("Repeater shoots two peas!");
            _timer = 0;
        }
        else
        {
            PlayAnimation(_idleAnim);
        }

        base.Update(gameTime);
    }

}
