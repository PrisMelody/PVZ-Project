using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Sprites;
public abstract class Sprite : ISprite
{
    protected ITextureAtlas Atlas;
    protected ITextureRegion Region;

    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; } = 0f;
    public Color Tint { get; set; } = Color.White;

    public Sprite(ITextureAtlas atlas, string regionName)
    {
        Atlas = atlas;
        Region = Atlas.GetRegion(regionName);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Atlas.Texture,
            Position,
            Region.SourceRectangle,
            Tint,
            Rotation,
            Region.Origin,
            Scale,
            SpriteEffects.None,
            0f
        );
    }
}


