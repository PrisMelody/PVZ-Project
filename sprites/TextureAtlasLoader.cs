using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Xml.Linq;   // For XDocument and XElement
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Sprites
{
    public class XmlTextureAtlasLoader : ITextureAtlasLoader
    {
        public TextureAtlas Load(ContentManager content, string filePath)
{
    string fullPath = Path.Combine("Content", filePath + ".xml");

    if (!File.Exists(fullPath))
        throw new FileNotFoundException($"Could not find XML file: {fullPath}");

    XDocument doc = XDocument.Load(fullPath);

    XElement root = doc.Element("TextureAtlas");
    if (root == null)
        throw new Exception("Root <TextureAtlas> not found in XML.");

    XElement texElement = root.Element("Texture");
    if (texElement == null)
        throw new Exception("<Texture> element not found in XML.");

    string textureName = texElement.Value.Trim();
    Texture2D texture = content.Load<Texture2D>(textureName);

    var atlas = new TextureAtlas(texture);

    foreach (XElement anim in root.Elements("Animation"))
    {
        foreach (XElement frame in anim.Elements("Frame"))
        {
            string name = frame.Attribute("name")?.Value;

            int x = int.Parse(frame.Attribute("x")?.Value ?? "0");
            int y = int.Parse(frame.Attribute("y")?.Value ?? "0");
            int w = int.Parse(frame.Attribute("width")?.Value ?? "0");
            int h = int.Parse(frame.Attribute("height")?.Value ?? "0");

            atlas.AddRegion(new TextureRegion(
                name,
                texture,
                new Rectangle(x, y, w, h)
            ));
        }
    }

    return atlas;
}
    }
}   