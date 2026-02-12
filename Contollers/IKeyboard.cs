using Microsoft.Xna.Framework.Input;

public interface IKeyboard : IController
{
    KeyboardState GetState();
}