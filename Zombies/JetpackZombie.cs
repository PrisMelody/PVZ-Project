using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class JetpackZombie : BasicZombie
{

    private float _height = 0f;
    public JetpackZombie(ITextureRegion region, float scale, int lane)
        : base(region, scale, lane)
    {
        Speed = 0.7f;
        Damage = 0;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _region.Texture,
            new Vector2(xCoord, yCoord - _height),
            _region.SourceRectangle,
            DrawColor,
            0.0f,
            Vector2.Zero,
            _scale,
            SpriteEffects.None,
            0.0f
        );
    }

    public override void Update(GameTime gameTime)
    {
        if (IsAttacking && _height < 75f)
        {
            _height++;
        }
        else if (!IsAttacking && _height > 0f)
        {
            _height -= .6f;
        }
        Move();
        IsAttacking = false;
    }
    

}