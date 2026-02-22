using System.Drawing;

public interface IGridPlot : IClickable, IPvZDrawable
{
    // The position of the plot on the grid
    Point GridPosition { get; }

    // The world position of the plot
    Point Position { get; }

    // Whether this plot is occupied by a plant
    bool IsOccupied { get; }

    // Whether this plot can have a plant placed on it
    bool CanPlacePlant { get; }

    // The plant currently on this plot (null if empty)
    IPlant Plant { get; }

    // Place a plant on this plot
    bool PlacePlant(IPlant plant);

    // Remove the plant from this plot
    void RemovePlant();

    // Check if a position is within this plot's bounds
    bool Contains(Point position);
}
