using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public interface IDrawable
{
    // The order of layer. For example, plants should be above lawn.
    int DrawOrder {get;}
    void Draw (SpriteBatch sprite);
}