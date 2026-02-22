using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace MonoGameLibrary.Sprites
{
    public interface ITextureAtlasLoader
    {
        TextureAtlas Load(ContentManager content, string filePath);
    }
}