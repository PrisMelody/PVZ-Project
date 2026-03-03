using System.Collections.Generic;

public class Lane : ILane
{
    private readonly List<IGridPlot> _plots = new List<IGridPlot>();

    public int RowIndex { get; }
    public IReadOnlyList<IGridPlot> Plots => _plots;

    public Lane(int rowIndex)
    {
        RowIndex = rowIndex;
    }

    public void AddPlot(IGridPlot plot)
    {
        _plots.Add(plot);
    }

    public IGridPlot GetPlotAtColumn(int column)
    {
        if (column < 0 || column >= _plots.Count)
            return null;
        return _plots[column];
    }
}
