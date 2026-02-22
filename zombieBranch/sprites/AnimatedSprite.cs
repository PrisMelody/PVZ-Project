
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MonoGameLibrary.Sprites;
public class AnimatedSprite
{
    private Animation _animation;
    private Vector2 _position;

    public AnimatedSprite(Animation animation, Vector2 position)
    {
        _animation = animation;
        _position = position;
    }

    public void Update(GameTime gameTime)
    {
        _animation.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _animation.CurrentFrame.Texture,
            _position,
            _animation.CurrentFrame.SourceRectangle,
            Color.White
        );
    }
}