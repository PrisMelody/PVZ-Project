public interface IShovel : IClickable, IPvZDrawable
{
    bool IsSelected { get; set; }

    void Activate();
    void Deactivate();
    void UseOnPlot(IGridPlot plot);
}
