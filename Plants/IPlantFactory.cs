using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IPlantFactory
{
    IPlant Create(PlantType type, float x, float y);
}
