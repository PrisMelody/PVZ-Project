using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MonoGameLibrary.Sprites;

public class Animation
{
    private List<ITextureRegion> _frames;
    private float _frameTime;
    private int _currentFrame;
    private float _timer;

    
    private float _speed = 1f;

    public ITextureRegion CurrentFrame => _frames[_currentFrame];

    public Animation(List<ITextureRegion> frames, float frameTime)
    {
        _frames = frames;
        _frameTime = frameTime;
    }

    
    public void SetSpeed(float speed)
    {
        _speed = Math.Max(0.01f, speed); 
    }

    public void Update(GameTime gameTime)
    {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        
        if (_timer >= _frameTime / _speed)
        {
            _timer = 0f;
            _currentFrame++;

            if (_currentFrame >= _frames.Count)
                _currentFrame = 0;
        }
    }

    public void Reset()
    {
        _currentFrame = 0;
        _timer = 0f;
    }
}