using Microsoft.Xna.Framework;

public interface IClickable
{
    Rectangle Bounds { get; }
    bool HitTest(Point mousePos);
    void OnClick(IMouse mouse);
}
