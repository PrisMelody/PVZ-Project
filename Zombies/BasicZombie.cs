using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public class BasicZombie : IZombie
{
    public bool IsAttacking {get; set;} = false;

    public int Speed {get; set;} = 1;
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
            TempZombieSpriteHandler.BasicZombie, 
            new Vector2(xCoord, yCoord), 
            null, 
            Color.White, 
            0.0f, 
            Vector2.Zero,
            1f,
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

//Current priority should be trying to get something that can appear on screen. After that:
//Deal with freezing/unfreezing (frozenState? Decorator? Could probably work with some if/else statments but that feels sloppy)
//Add other zombie types. Should it be decorators or states?

