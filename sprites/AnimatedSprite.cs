using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Sprites;

public class AnimatedSprite
{
    private Animation _animation;
    private Vector2 _position;
    private float _scale = 1f;

    public AnimatedSprite(Animation animation, Vector2 position)
    {
        _animation = animation;
        _position = position;
    }

    public void Update(GameTime gameTime)
    {
        _animation.Update(gameTime);
    }

    public void SetAnimation(Animation animation)
    {
        if (_animation == animation) return;

        _animation = animation;
        _animation.Reset();
    }

    public void SetScale(float scale)
    {
        _scale = scale;
    }
    public Animation CurrentAnimation => _animation;

    public void Draw(SpriteBatch spriteBatch)
    {
        var frame = _animation.CurrentFrame;

        Vector2 origin = Vector2.Zero;

        spriteBatch.Draw(
            frame.Texture,
            _position,
            frame.SourceRectangle,
            Color.White,
            0f,
            origin,
            _scale,
            SpriteEffects.None,
            0f
        );
    }
}