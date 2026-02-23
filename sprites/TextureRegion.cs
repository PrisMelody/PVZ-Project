using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Sprites;
public class TextureRegion : ITextureRegion
{
    public string Name { get; private set; }
    public Rectangle SourceRectangle { get; private set; }
    public Vector2 Origin { get; private set; }
    public Texture2D Texture { get; private set; }

    public TextureRegion(string name, Texture2D texture, Rectangle rect, Vector2? origin = null)
    {
        Name = name;
        Texture = texture;
        SourceRectangle = rect;
        Origin = origin ?? Vector2.Zero;
    }
}