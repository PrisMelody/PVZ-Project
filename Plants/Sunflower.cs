using MonoGameLibrary.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


public class Sunflower : Plant
{
    private double _timer = 0;

    

    public Sunflower(Animation idle, Animation action, float x, float y)
        : base(idle, action, x, y)
    {
    }

    public override void Update(GameTime gameTime)
    {
        _timer += gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer > 5)
        {
            PlayAnimation(_actionAnim); // producing
            ProduceSun();
            _timer = 0;
        }
        else
        {
            PlayAnimation(_idleAnim);
        }

        base.Update(gameTime);
    }

    private void ProduceSun()
    {
        System.Console.WriteLine("Sun produced!");
    }
}