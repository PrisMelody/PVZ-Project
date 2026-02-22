using System.Drawing;
using Microsoft.Xna.Framework.Graphics;

public class Shovel : IShovel
{
    public bool IsSelected { get; set; }
    public Rectangle Bounds { get; private set; }
    public int DrawOrder { get; set; }

    private Point position;

    public Shovel(Point position, Rectangle bounds)
    {
        this.position = position;
        Bounds = bounds;
        IsSelected = false;
        DrawOrder = 100; // High draw order for UI elements
    }

    public void Draw(SpriteBatch sprite)
    {
        // TODO: Draw shovel sprite
        // This will be implemented when sprites are available
    }

    public void Activate()
    {
        IsSelected = true;
    }

    public void Deactivate()
    {
        IsSelected = false;
    }

    public bool HitTest(Point mousePos)
    {
        return Bounds.Contains(mousePos);
    }

    public void OnClick(InputState input)
    {
        if (HitTest(input.MousePosition))
            IsSelected = !IsSelected;
    }

    public void UseOnPlot(IGridPlot plot)
    {
        if (plot != null && plot.IsOccupied)
        {
            plot.RemovePlant();
        }
    }
}
