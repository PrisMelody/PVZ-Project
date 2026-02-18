using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
public interface IDraggable
{
    // If the object is being dragged
    bool IsDragging { get; }
    // The bound of the draggable objects.
    Rectangle Bounds { get; } 
    // Get the state of the mouse. 
    void OnDragStart(InputState input);
    void OnDrag(InputState input);
    void OnDragEnd(InputState input);
}