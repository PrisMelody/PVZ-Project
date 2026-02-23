using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IMap
{
    int SelectedPlantType { get; }

    void SelectPlant(int plantType);
    void ClearPlant();

    /// <summary>Returns the seed packet index at the screen position, or -1 if none.</summary>
    int GetSeedPacketIndexAt(System.Drawing.Point screenPos);

    /// <summary>Returns true if the shovel was clicked.</summary>
    bool IsShovelAt(System.Drawing.Point screenPos);

    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}
