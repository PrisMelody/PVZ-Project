using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class GridManager : IGridManager
{
    private readonly List<Lane> _lanes = new List<Lane>();
    private readonly List<IGridPlot> _allPlots = new List<IGridPlot>();

    public int Rows { get; private set; }
    public int Columns { get; private set; }
    public IReadOnlyList<ILane> Lanes => _lanes;
    public IReadOnlyList<IGridPlot> AllPlots => _allPlots;

    public IEnumerable<IGridPlot> OccupiedPlots =>
        _allPlots.Where(p => p.IsOccupied);

    public GridManager() { }

    /// <summary>
    /// Builds the grid: rows × columns plots. Each plot is created with grid position (row, col)
    /// and world position (originX + col * cellWidth, originY + row * cellHeight).
    /// Bounds are the same size as the cell for each plot.
    /// </summary>
    public void Initialize(int rows, int columns, int cellWidth, int cellHeight, Point origin)
    {
        if (rows <= 0 || columns <= 0 || cellWidth <= 0 || cellHeight <= 0)
            throw new ArgumentException("Rows, columns, and cell dimensions must be positive.");

        _lanes.Clear();
        _allPlots.Clear();
        Rows = rows;
        Columns = columns;

        for (int r = 0; r < rows; r++)
        {
            var lane = new Lane(r);
            for (int c = 0; c < columns; c++)
            {
                var gridPos = new Point(c, r);
                var worldX = origin.X + c * cellWidth;
                var worldY = origin.Y + r * cellHeight;
                var position = new Point(worldX, worldY);
                var bounds = new Rectangle(worldX, worldY, cellWidth, cellHeight);

                var plot = new GridPlot(gridPos, position, bounds);
                lane.AddPlot(plot);
                _allPlots.Add(plot);
            }
            _lanes.Add(lane);
        }
    }

    public ILane GetLane(int row)
    {
        if (row < 0 || row >= _lanes.Count)
            return null;
        return _lanes[row];
    }

    public IGridPlot GetPlot(int row, int column)
    {
        var lane = GetLane(row);
        return lane?.GetPlotAtColumn(column);
    }

    public IGridPlot GetPlotAt(Point screenPosition)
    {
        foreach (var plot in _allPlots)
        {
            if (plot.Contains(screenPosition))
                return plot;
        }
        return null;
    }
}
