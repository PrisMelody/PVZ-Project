using MonoGameLibrary.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class Chomper : Plant
{
    private bool _isAttacking;

    public Chomper(Animation idle, Animation action, float x, float y)
        : base(idle, action, x, y)
    {
    _sprite.SetScale(2.0f); 
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_isAttacking)
            PlayAnimation(_actionAnim);
        else
            PlayAnimation(_idleAnim);
    }

    public void TriggerAttack()
    {
        _isAttacking = true;
    }

    public void StopAttack()
    {
        _isAttacking = false;
    }
}