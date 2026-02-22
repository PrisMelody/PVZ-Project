using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
public interface ITextureRegion
{
    string Name { get; }
    Rectangle SourceRectangle { get; }
    Vector2 Origin { get; }
    Texture2D Texture { get; }
}