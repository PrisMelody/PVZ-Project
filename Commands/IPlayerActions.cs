using Microsoft.Xna.Framework;

/// <summary>
/// Contract for game actions that commands execute. Keeps commands decoupled from Game1.
/// </summary>
public interface IPlayerActions
{
    void SetSelectedPlant(int plantType);
    void PlacePlant(Point gridOrScreenPosition); // For full game: place on grid
    void ClearPlant(); // Shovel: show no plant (used this sprint for single-plot)
}
