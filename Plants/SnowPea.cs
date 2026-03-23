using MonoGameLibrary.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class SnowPea : Plant
{
    private double _timer;

    public SnowPea(Animation idle, Animation action, float x, float y)
        : base(idle, action, x, y) // health example
    {
        _timer = 0;
    }

    public override void Update(GameTime gameTime)
    {
        _timer += gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer > 5) // every 5 seconds
        {
            PlayAnimation(_actionAnim); // same animation for now
            System.Console.WriteLine("SnowPea shoots a frozen pea!");
            _timer = 0;
        }
        else
        {
            PlayAnimation(_idleAnim);
        }

        base.Update(gameTime);
    }
}