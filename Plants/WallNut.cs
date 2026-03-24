using MonoGameLibrary.Sprites;
using Microsoft.Xna.Framework;
using System;

public class WallNut : Plant
{
    public WallNut(Animation idle, Animation action, float x, float y)
        : base(idle, action, x, y) // example health
    {
    }

    public override void Update(GameTime gameTime)
    {
        // No behavior for now
        base.Update(gameTime);
    }
}