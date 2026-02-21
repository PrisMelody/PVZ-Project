using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Sprites;
public class PlantSprite : ISprite
{
    public TextureRegion Region { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Origin { get; set; }
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0f;
    public Vector2 Scale { get; set; } = Vector2.One;
    public float LayerDepth { get; set; } = 0f;

    public PlantSprite(TextureRegion region, Vector2 position)
    {
        Region = region;
        Position = position;
        Origin = new Vector2(region.Width / 2f, region.Height / 2f);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Region.Texture,
            Position,
            Region.SourceRectangle,
            Color,
            Rotation,
            Origin,
            Scale,
            SpriteEffects.None,
            LayerDepth);
    }
}


