using Microsoft.Xna.Framework;

public interface IGridPlot : IClickable, IPvZDrawable
{
    Point GridPosition { get; }
    Point Position { get; }
    bool IsOccupied { get; }
    bool CanPlacePlant { get; }
    IPlant Plant { get; }

    bool PlacePlant(IPlant plant);
    void RemovePlant();
    bool Contains(Point position);
}
