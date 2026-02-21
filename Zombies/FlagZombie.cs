using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
public class FlagZombie : BasicZombie
{
    public FlagZombie(float x, float y) : base(x, y)
    {
        xCoord = x;
        yCoord = y;
        Speed = 2;
    }

    override public void Draw (SpriteBatch spriteBatch)
    {    //This is a placeholder using a static class instead of a dedicated sprite handling setup.
        spriteBatch.Draw(
            TempZombieSpriteHandler.FlagZombie, 
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
}