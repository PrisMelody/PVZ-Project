using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IMap
{
    PlantType? SelectedPlantType { get; }

    void SelectPlant(PlantType type);
    void ClearPlant();

    /// <summary>Tries to place the selected plant on the plot at the given screen position. Returns true if placed.</summary>
    bool TryPlacePlantAt(Point screenPos);

    /// <summary>Tries to remove the plant at the given screen position. Returns true if removed.</summary>
    bool TryRemovePlantAt(Point screenPos);

    int GetSeedPacketIndexAt(Point screenPos);
    bool IsShovelAt(Point screenPos);

    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}
