using Microsoft.Xna.Framework.Input;

public interface IMouse : IController
{
    MouseState GetState();
}