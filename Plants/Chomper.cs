using MonoGameLibrary.Sprites;
using Microsoft.Xna.Framework;
using System;

public class Chomper : Plant
{
    public Chomper(Animation idle, Animation action, float x, float y)
        : base(idle, action, x, y) // example health
    {
    }

    public override void Update(GameTime gameTime)
    {
        // No behavior for now
        base.Update(gameTime);
    }
}