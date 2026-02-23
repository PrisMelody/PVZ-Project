using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
public interface ITextureAtlas
{
    Texture2D Texture { get; }

    // Get a region by name
    ITextureRegion GetRegion(string name);

    // Optional helpers
    bool HasRegion(string name);
    IEnumerable<string> GetRegionNames();
}