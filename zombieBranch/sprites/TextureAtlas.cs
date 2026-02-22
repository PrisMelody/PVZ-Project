using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
using System.Collections.Generic;

using System.Linq;

namespace MonoGameLibrary.Sprites;
public class TextureAtlas : ITextureAtlas
{
    public Texture2D Texture { get; private set; }
    private Dictionary<string, ITextureRegion> regions = new Dictionary<string, ITextureRegion>();

    public TextureAtlas(Texture2D texture)
    {
        Texture = texture;
    }

    public void AddRegion(ITextureRegion region)
    {
        regions[region.Name] = region;
    }

    public ITextureRegion GetRegion(string name)
    {
        if (!regions.ContainsKey(name))
            throw new Exception($"Region '{name}' not found in atlas");
        return regions[name];
    }
    public List<ITextureRegion> GetRegionsStartingWith(string prefix)
    {
    return regions
        .Where(kvp => kvp.Key.StartsWith(prefix)) // filter by dictionary key
        .Select(kvp => kvp.Value)                // get the ITextureRegion
        .ToList();
    }

    public bool HasRegion(string name) => regions.ContainsKey(name);
    public IEnumerable<string> GetRegionNames() => regions.Keys;
}