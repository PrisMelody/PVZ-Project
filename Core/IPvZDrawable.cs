using Microsoft.Xna.Framework.Graphics;

public interface IPvZDrawable
{
    int DrawOrder { get; }
    void Draw(SpriteBatch sprite);
}
