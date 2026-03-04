using Microsoft.Xna.Framework;

public interface IPlayerActions
{
    void SetSelectedPlant(PlantType type);
    void PlacePlant(Point screenPosition);
    void ClearPlant();
    void UseShovel(Point screenPosition);
}
