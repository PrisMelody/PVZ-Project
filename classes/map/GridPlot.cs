using System.Drawing;
using Microsoft.Xna.Framework.Graphics;

public class GridPlot : IGridPlot
{
    public Point GridPosition { get; private set; }
    public Point Position { get; private set; }
    public bool IsOccupied { get; private set; }
    public bool CanPlacePlant { get; private set; }
    public IPlant Plant { get; private set; }
    public Rectangle Bounds { get; private set; }
    public int DrawOrder { get; set; }

    public Plot(Point gridPosition, Point position, Rectangle bounds)
    {
        GridPosition = gridPosition;
        Position = position;
        Bounds = bounds;
        IsOccupied = false;
        CanPlacePlant = true;
        Plant = null;
        DrawOrder = 10; // Low draw order, drawn before plants
    }

    public void Draw(SpriteBatch sprite)
    {
        // TODO: Draw plot sprite/background
        // This will be implemented when sprites are available

        // Draw plant if occupied
        if (IsOccupied && Plant != null && Plant is IDrawable drawablePlant)
        {
            drawablePlant.Draw(sprite);
        }
    }

    public bool PlacePlant(IPlant plant)
    {
        if (CanPlacePlant && !IsOccupied && plant != null)
        {
            Plant = plant;
            IsOccupied = true;
            CanPlacePlant = false;
            return true;
        }
        return false;
    }

    public void RemovePlant()
    {
        if (IsOccupied)
        {
            Plant = null;
            IsOccupied = false;
            CanPlacePlant = true;
        }
    }

    public bool Contains(Point position)
    {
        return Bounds.Contains(position);
    }
}
