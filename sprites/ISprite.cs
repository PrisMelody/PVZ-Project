
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
public interface ISprite
{
    Vector2 Position { get; set; }
    Vector2 Scale { get; set; }
    float Rotation { get; set; }
    Color Tint { get; set; }

    void Draw(SpriteBatch spriteBatch);
}