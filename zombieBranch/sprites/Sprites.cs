using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Sprites;
public abstract class Sprite
{
    protected ITextureRegion _region;
    protected Vector2 _position;

    protected Sprite(ITextureRegion region, Vector2 position)
    {
        _region = region;
        _position = position;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _region.Texture,
            _position,
            _region.SourceRectangle,
            Color.White
        );
    }

    public abstract void Update(GameTime gameTime);
}


