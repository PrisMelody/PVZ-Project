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
    private bool _looping;

    
    private float _speed = 1f;

    public ITextureRegion CurrentFrame => _frames[_currentFrame];

    public Animation(List<ITextureRegion> frames, float frameTime, bool looping = true)  
    {
        _frames = frames;
        _frameTime = frameTime;
        _looping = looping;
    }

    
    public void SetSpeed(float speed)
    {
        _speed = Math.Max(0.01f, speed); 
    }
    public void SetLooping(bool looping)
    {
        _looping = looping;
    }
    public void SetFrame(int index)
    {
        if (index < 0 || index >= _frames.Count)
            return;

        _currentFrame = index;
        _timer = 0f; 
    }

     public void Update(GameTime gameTime)
    {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer >= _frameTime / _speed)
        {
            _timer = 0f;

            if (_looping)
            {
                _currentFrame = (_currentFrame + 1) % _frames.Count;
            }
            else
            {
                if (_currentFrame < _frames.Count - 1)
                    _currentFrame++;
            }
        }
    }


    public void Reset()
    {
        _currentFrame = 0;
        _timer = 0f;
    }
    public bool IsFinished => _currentFrame == _frames.Count - 1;
}