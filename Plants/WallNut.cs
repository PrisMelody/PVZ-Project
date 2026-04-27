using MonoGameLibrary.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
public class WallNut : Plant
{   private double _testTimer = 0;
    public WallNut(Animation idle, Animation action, float x, float y)
        : base(idle, action, x, y)
    {
        _sprite.SetScale(0.75f);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        _testTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (IsDead)
            return;

    if (_testTimer > 2)
    {
        TestDamage(40); 
        _testTimer = 0; // reset timer to test multiple damage instances
    }
    if (_testTimer > 4)
    {
        TestDamage(40); 
        _testTimer = 0; // reset timer to test multiple damage instances
        
    }
    

        float hpPercent = (float)Health / 100f;

        if (hpPercent > 0.5f)
            _idleAnim.SetFrame(0);
        else if (hpPercent > 0.25f)
            _idleAnim.SetFrame(1);
        else  
            _idleAnim.SetFrame(2);
        


        PlayAnimation(_idleAnim);
    }
    public void TestDamage(int amount)
    {
        TakeDamage(amount);
    }
}
