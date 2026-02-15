using System;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary.Sprites;

public class AnimatedSprite : PlantSprite 
{
    private int _currentFrame;
    private TimeSpan _elapsed;
    private Animation _animation;

    
    public Animation Animation
    {
        get => _animation;
        set
        {
            _animation = value;
            Region = _animation.Frames[0];
        }
    }
    
    public AnimatedSprite() { }

    
    public AnimatedSprite(Animation animation)
    {
        Animation = animation;
    }
   
    public void Update(GameTime gameTime)
    {
        _elapsed += gameTime.ElapsedGameTime;
        FrameDelay(gameTime);

        
    }
    public void UnexpectedFrame()
    {
        if (_currentFrame >= _animation.Frames.Count)
            {
                _currentFrame = 0;
            }
    }
    public void Reset()
    {
        _currentFrame = 0;
        _elapsed = TimeSpan.Zero;
        Region = _animation.Frames[0];
    }   
     public void FrameDelay(GameTime gameTime)
    {
        if (_elapsed >= _animation.Delay)
        {
            _elapsed -= _animation.Delay;
            _currentFrame++;

            UnexpectedFrame();

            Region = _animation.Frames[_currentFrame];
        }
        
    }



}
