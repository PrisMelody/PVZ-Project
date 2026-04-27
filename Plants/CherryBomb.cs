using MonoGameLibrary.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class CherryBomb : Plant
{
    private List<Projectile> _projectiles;
    private Texture2D _pow;
    private bool _exploded = false;
    public CherryBomb(Animation idle, Animation action, float x, float y,
                  List<Projectile> projectiles, Texture2D pow)
        : base(idle, action, x, y) // example health
    {
        _sprite.SetScale(.2f);
        _projectiles = projectiles;
        _pow = pow;
    }

     public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // Prevent multiple explosions
        if (_exploded)
            return;

        // Trigger explosion when animation finishes (same pattern as PotatoMine)
        if (_sprite.CurrentAnimation.IsFinished)
            {
                Explode();
            }
    }

    private void Explode()
    {
        _exploded = true;

        // spawn explosion effect (your "pow")
        _projectiles.Add(new Pow(
            XPos,
            YPos,
            _pow
        ));

        // remove plant
        IsDead = true;
    }
}