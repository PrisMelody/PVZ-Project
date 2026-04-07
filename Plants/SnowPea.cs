using MonoGameLibrary.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class SnowPea : Plant
{
    private double _timer;
    private List<Projectile> _projectiles;
    private Texture2D _peaTexture;

    public SnowPea(Animation idle, Animation action, float x, float y,
    List<Projectile> projectiles, Texture2D SnowPeaTexture)
        : base(idle, action, x, y) // health example
    {
        _projectiles = projectiles;
        _peaTexture = SnowPeaTexture;
        _timer = 0;
    }

    public override void Update(GameTime gameTime)
    {
        _timer += gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer > 1.425) // TODO: remove magic numbers from this and other plants.
        {
            PlayAnimation(_actionAnim); // same animation for now
            var pea = new Pea(XPos + 40, YPos + 20, _peaTexture); 
            _projectiles.Add(pea);
            _timer = 0;
        }
        else
        {
            PlayAnimation(_idleAnim);
        }

        base.Update(gameTime);
    }
}