using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public class BasicZombie : IZombie
//TODO: add some way to handle freezing, presumably this will mostly just slow how often updates occur.
{
    public bool IsAttacking {get; set;} = false;

    public float Speed {get; set;} = 0.5f;
    public float xCoord {get; set;}
    public float yCoord {get; set;}

    public int Health{get; set;} = 270;
    public bool IsDead {get; set;}

    public int DrawOrder {get; set;}

    public BasicZombie(float x, float y)
    {
        xCoord = x;
        yCoord = y;
    }


    public void Move()
    {
        xCoord -= Speed;
    }

    public void Attack()
    {
        //Currently blank, will likely remain so until Sprint 3.
    }

    public void TakeDamage (int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            IsDead = true;
        }
    }


    virtual public void Draw (SpriteBatch spriteBatch)
    {    //This is a placeholder using a static class instead of a dedicated sprite handling setup.
        spriteBatch.Draw(
            TempZombieSpriteHandler.Zombies, 
            new Vector2(xCoord, yCoord), 
            new Rectangle(475, 42, 86, 153), 
            Color.White, 
            0.0f, 
            Vector2.Zero,
            0.5f,
            SpriteEffects.None,
            0.0f //For now this is just a constant, later it should use drawOrder, or whatever we go with.
        );
    }

    public void Update(GameTime gameTime)
    {
    if (!IsAttacking){
            Move();
            //for sprint 3, we'll add some kind of check here to see if there are plants, in which case it will switch to attack mode.
        }
        else
        {
            Attack();
        }
    }
}



