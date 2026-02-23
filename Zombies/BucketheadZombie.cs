using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//Wrapper for BasicZombie that adds a bucket. Yippee!
public class BucketheadZombie : IZombie
{
    private IZombie WrappedZombie;

    public float Speed {
        get{return WrappedZombie.Speed;}
        set{WrappedZombie.Speed = value;}}

    public float xCoord {
        get{return WrappedZombie.xCoord;}
        set{WrappedZombie.xCoord = value;}}

    public float yCoord{
        get{return WrappedZombie.yCoord;}
        set{WrappedZombie.yCoord = value;}}

    public int Health{
        get{return WrappedZombie.Health;}
        set{WrappedZombie.Health = value;}
        }

    public bool IsDead {
        get{return WrappedZombie.IsDead;}
        set{WrappedZombie.IsDead = value;}
        }

     public bool IsAttacking {
        get{return WrappedZombie.IsAttacking;}
        set{WrappedZombie.IsAttacking = value;}}


    public int DrawOrder{
        get{return WrappedZombie.DrawOrder;}
        set{WrappedZombie.DrawOrder = value;}
        }


    public BucketheadZombie(float x, float y)
    {
        WrappedZombie = new BasicZombie(x, y);
        WrappedZombie.Health = 1370;
    }


    public void Move()
    {
        WrappedZombie.Move();
    }

    public void Attack()
    {
        WrappedZombie.Attack();
    }
    
    public void TakeDamage (int amount)
    {
        WrappedZombie.TakeDamage(amount);
        if ((Health <= 270) && (Health + amount > 270)) //Basically checking if the health has dropped below the threshhold.
        {
            //Insert bucket spawn logic here.
            System.Console.WriteLine("This... is a bucket. ");
        }
    }

    public void Draw (SpriteBatch spriteBatch)
    {    //This is a placeholder using a static class instead of a dedicated sprite handling setup.
        if (Health <= 270)
        {
            WrappedZombie.Draw(spriteBatch);
        }
        else{
            spriteBatch.Draw(
                TempZombieSpriteHandler.Zombies, 
                new Vector2(xCoord, yCoord), 
                new Rectangle(238, 16, 96, 179), 
                Color.White, 
                0.0f, 
                Vector2.Zero,
                0.5f,
                SpriteEffects.None,
                0.0f //For now this is just a constant, later it should use drawOrder, or whatever we go with.
            );
        }
    }

    public void Update(GameTime gameTime)
    {
        WrappedZombie.Update(gameTime);
    }
}