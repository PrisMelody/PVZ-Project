using MonoGameLibrary.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


public class Sunflower : Plant
{
    private double _timer = 0;
    private List<SunCollectable> _sun;
    private Texture2D _sunTexture;

    

    public Sunflower(Animation idle, Animation action, float x, float y,
     List<SunCollectable> sun, Texture2D sunTexture)
        : base(idle, action, x, y)
    {
        _sun = sun;
        _sunTexture = sunTexture;
    }

    public override void Update(GameTime gameTime)
    {
        _timer += gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer > 5)
        {
            PlayAnimation(_actionAnim); // producing
            ProduceSun();
            _timer = 0;
        }
        else
        {
            PlayAnimation(_idleAnim);
        }

        base.Update(gameTime);
    }

    private void ProduceSun() //TODO: this doesn't currently work properly with the updated sun collectable system.
    {
        var sun = new SunCollectable(
        new Point((int)XPos + 60, (int)YPos + 60), // tweak this
        25,
        5f,
        new Rectangle(0, 0, 60, 60),
        false,
        _sunTexture

    );
        _sun.Add(sun);
    }
}