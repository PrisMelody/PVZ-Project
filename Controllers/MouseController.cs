using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class MouseController : IMouse
{
    private MouseState _previousState;
    private readonly IGameInputHandler _handler;

    public MouseController(IGameInputHandler handler)
    {
        _handler = handler ?? throw new System.ArgumentNullException(nameof(handler));
    }

    public MouseState GetState()
    {
        return Mouse.GetState();
    }

    public void Update()
    {
        MouseState currentState = Mouse.GetState();

        if (currentState.LeftButton == ButtonState.Pressed &&
            _previousState.LeftButton == ButtonState.Released)
        {
            _handler.HandleClick(currentState.Position);
        }

        _previousState = currentState;
    }
}
