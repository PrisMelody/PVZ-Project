using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IPlant : IDamageable, IPvZDrawable, IPvZUpdatable
{
    float XPos { get; set; }
    float YPos { get; set; }
}
