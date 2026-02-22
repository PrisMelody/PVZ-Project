using System.Drawing;

public interface IShovel : IClickable, IPvZDrawable
{
    // Whether the shovel is currently selected/active
    bool IsSelected { get; set; }

    // Activate the shovel tool
    void Activate();

    // Deactivate the shovel tool
    void Deactivate();

    // Use the shovel on a plot (removes plant from plot)
    void UseOnPlot(IGridPlot plot);
}
