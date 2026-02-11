using System.Drawing;

public interface IClickable
{
    // The clickable area/bound. Should be a shape of triangle for our game.
    Rectangle Bounds {get;}
    // If mouse is inside the bound of the clickable area.
    bool HitTest (Point mousePos);
    /* 
    * Get the state of mouse. 
    * class InputState should be setup by teammates responsible for controller.
    ChatGPT has provide a possible working InputState class which might be able to use as reference.
    using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public sealed class InputState
{
    public MouseState Mouse { get; private set; }
    public MouseState PrevMouse { get; private set; }
    public KeyboardState Keyboard { get; private set; }
    public KeyboardState PrevKeyboard { get; private set; }

    public Point MousePosition => Mouse.Position;

    public bool LeftPressedThisFrame =>
        Mouse.LeftButton == ButtonState.Pressed &&
        PrevMouse.LeftButton == ButtonState.Released;

    public bool LeftReleasedThisFrame =>
        Mouse.LeftButton == ButtonState.Released &&
        PrevMouse.LeftButton == ButtonState.Pressed;

    public bool LeftDown => Mouse.LeftButton == ButtonState.Pressed;

    public void Update()
    {
        PrevMouse = Mouse;
        PrevKeyboard = Keyboard;
        Mouse = Mouse.GetState();
        Keyboard = Keyboard.GetState();
    }
}
*/
    void OnClick (InputState input);
}