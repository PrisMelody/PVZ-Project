using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MonoGameLibrary.Sprites;

public class TestSprite : Sprite
{
    public TestSprite(ITextureRegion region, Vector2 position)
        : base(region, position)
    {
    }

    public override void Update(GameTime gameTime)
    {
        // no movement needed
    }
}