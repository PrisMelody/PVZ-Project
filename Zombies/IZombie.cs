using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace PlantsVsZombies.Zombies
{

public interface IZombie :  IDamageable, IPvZDrawable, IPvZUpdatable
{
    float Speed{get; set;}
    bool IsAttacking{get; set;}

    float xCoord{get; set;}
    float yCoord{get; set;}

Rectangle Hitbox { get; }
    void Move();
    void Attack();
}
}