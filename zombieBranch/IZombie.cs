using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


interface IZombie :  IDamageable, IDrawable, IUpdateable //Was commented out due to weirdness.
{
    int Speed{get; set;}
    bool IsAttacking{get; set;}

    float xCoord{get; set;}
    float yCoord{get; set;}

    void Move();
    void Attack();

    //void Update (GameTime gameTime); //Currently here due to weirdness with Updatable.
}