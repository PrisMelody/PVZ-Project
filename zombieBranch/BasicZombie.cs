using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public class BasicZombie : IZombie
{
    public bool IsAttacking {get; set;} = false;

    public int Speed {get; set;} = 1;
    public float xCoord {get; set;}
    public float yCoord {get; set;}
// Che: Why not just use vecter2?
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

/*
Che: We eventually will need a delta time, or our game speed would be hooked to frame speed.
For example
public void Move(GameTime gameTime)
{
    xCoord -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
}

// In the update method:
this.Move(gameTime);
*/
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


    public void Draw (SpriteBatch spriteBatch)
    {    //This is a placeholder using a static class instead of a dedicated sprite handling setup.
        spriteBatch.Draw(
            TempZombieSpriteHandler.FlagZombie, 
            new Vector2(xCoord, yCoord), 
            null, 
            Color.White, 
            0.0f, 
            Vector2.Zero,
            0.1f,
            SpriteEffects.None,
            0.0f //For now this is just a constant, later it should use drawOrder, or whatever we go with.
        

        );
    }

    public void Update(GameTime gameTime)
    {
    if (!IsAttacking){
            this.Move();
            //for sprint 3, we'll add some kind of check here to see if there are plants, in which case it will switch to attack mode.
        }
        else
        {
            this.Attack();
        }
    }
}

//Current priority should be trying to get something that can appear on screen. After that:
//Deal with freezing/unfreezing (frozenState? Decorator? Could probably work with some if/else statments but that feels sloppy)
//Add other zombie types. Should it be decorators or states?

