
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MonoGameLibrary.Sprites;
public class Animation
{
    private List<ITextureRegion> _frames;
    private float _frameTime;
    private int _currentFrame;
    private float _timer;

    public ITextureRegion CurrentFrame => _frames[_currentFrame];

    public Animation(List<ITextureRegion> frames, float frameTime)
    {
        _frames = frames;
        _frameTime = frameTime;
    }

    public void Update(GameTime gameTime)
    {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer >= _frameTime)
        {
            _timer = 0f;
            _currentFrame++;

            if (_currentFrame >= _frames.Count)
                _currentFrame = 0;
        }
    }
}