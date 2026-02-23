using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IProjectile : IPvZDrawable, IPvZUpdatable
{
    int Damage { get; }
    float Speed { get; }
    float XPos { get; set; }
    float YPos { get; set; }

    void Move();
}
