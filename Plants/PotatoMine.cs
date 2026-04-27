using MonoGameLibrary.Sprites;
using Microsoft.Xna.Framework;
using System;

public class PotatoMine : Plant
{
    public PotatoMine(Animation idle, Animation action, float x, float y)
        : base(idle, action, x, y) // example health
    {
        _sprite.SetScale(.2f);
    }

    public override void Update(GameTime gameTime)
    {
        // No behavior for now
        base.Update(gameTime);
    }
}