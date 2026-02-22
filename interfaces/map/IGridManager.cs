using System.Collections.Generic;
using System.Drawing;

public interface IGridManager
{
    /// <summary>Number of rows (lanes).</summary>
    int Rows { get; }

    /// <summary>Number of columns per row.</summary>
    int Columns { get; }

    /// <summary>All lanes from row 0 to Rows-1.</summary>
    IReadOnlyList<ILane> Lanes { get; }

    /// <summary>Gets the lane for the given row index.</summary>
    ILane GetLane(int row);

    /// <summary>Gets the plot at (row, column), or null if out of range.</summary>
    IGridPlot GetPlot(int row, int column);

    /// <summary>Finds the plot that contains the given screen position; null if none.</summary>
    IGridPlot GetPlotAt(Point screenPosition);

    /// <summary>All plots in row-major order (row 0, then row 1, ...).</summary>
    IReadOnlyList<IGridPlot> AllPlots { get; }

    /// <summary>All plots that currently have a plant.</summary>
    IEnumerable<IGridPlot> OccupiedPlots { get; }
}
