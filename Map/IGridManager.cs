using System.Collections.Generic;
using Microsoft.Xna.Framework;

public interface IGridManager
{
    int Rows { get; }
    int Columns { get; }
    IReadOnlyList<ILane> Lanes { get; }

    ILane GetLane(int row);
    IGridPlot GetPlot(int row, int column);
    IGridPlot GetPlotAt(Point screenPosition);

    IReadOnlyList<IGridPlot> AllPlots { get; }
    IEnumerable<IGridPlot> OccupiedPlots { get; }
}
