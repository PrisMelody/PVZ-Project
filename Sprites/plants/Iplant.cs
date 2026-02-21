using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MonoGameLibrary.Sprites;  
public interface IPlant: IDrawable,IDamageable
{
    bool IsAttacking{get; set;}
    float xCoord{get; set;}
    float yCoord{get; set;}
    
    public void Update(GameTime gameTime);
    public void Draw(SpriteBatch spriteBatch);
}