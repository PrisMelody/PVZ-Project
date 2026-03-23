using MonoGameLibrary.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
public class Repeater : Plant
{
    private double _timer;
    private List<Projectile> _projectiles;
    private Texture2D _peaTexture;

    public Repeater(Animation idle, Animation action, float x, float y,
    List<Projectile> projectiles, Texture2D peaTexture)
        : base(idle, action, x, y) // health example
        
    {
        _projectiles = projectiles;
        _peaTexture = peaTexture;
        _timer = 0;
    }

    public override void Update(GameTime gameTime)
    {
        _timer += gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer > 5) // every 5 seconds
        {
            PlayAnimation(_actionAnim); // same animation for now
            System.Console.WriteLine("Repeater shoots two peas!");
            var pea1 = new Pea(XPos + 40, YPos + 20, _peaTexture);
            var pea2 = new Pea(XPos + 40, YPos + 50, _peaTexture);

            _projectiles.Add(pea1);
            _projectiles.Add(pea2);
            _timer = 0;
        }
        else
        {
            PlayAnimation(_idleAnim);
        }

        base.Update(gameTime);
    }
}