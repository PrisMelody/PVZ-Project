using System.Drawing;

public interface IDraggable
{
    bool IsDragging { get; }
    Rectangle Bounds { get; }
    void OnDragStart(MouseController mouse);
    void OnDrag(MouseController mouse);
    void OnDragEnd(MouseController mouse);
}
