using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MonoGameLibrary.Sprites;
public class Peashoter : PlantSprite, IPlant
{
    public bool IsAttacking { get; set; } = false;

    public Peashoter(TextureRegion region, float x, float y)
        : base(region, new Vector2(x, y))
    {
    }

    public void Update(GameTime gameTime)
    {
        // attack logic here
    }
}