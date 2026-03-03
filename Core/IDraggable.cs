using Microsoft.Xna.Framework;

public interface IDraggable
{
    bool IsDragging { get; }
    Rectangle Bounds { get; }
    void OnDragStart(IMouse mouse);
    void OnDrag(IMouse mouse);
    void OnDragEnd(IMouse mouse);
}
