using System.Collections.Generic;

public interface ILane
{
    /// <summary>Row index of this lane (0 = front, e.g. bottom row in PvZ).</summary>
    int RowIndex { get; }

    /// <summary>Plots in this lane from left (column 0) to right.</summary>
    IReadOnlyList<IGridPlot> Plots { get; }

    /// <summary>Gets the plot at the given column, or null if out of range.</summary>
    IGridPlot GetPlotAtColumn(int column);
}
