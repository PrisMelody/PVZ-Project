using MonoGameLibrary.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
public class WallNut : Plant
{
    public WallNut(Animation idle, Animation action, float x, float y)
        : base(idle, action, x, y) // example health
    {
        _sprite.SetScale(0.75f);
    }
    

    public override void Update(GameTime gameTime)
    {
        // No behavior for now
        base.Update(gameTime);
    }
}