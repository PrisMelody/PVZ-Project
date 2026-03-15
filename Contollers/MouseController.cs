using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class MouseController : IMouse
{
    private MouseState _previousState;
    private readonly IGameInputHandler _handler;

    private bool _isDragging = false;

    public MouseController(IGameInputHandler handler)
    {
        _handler = handler;
    }

    public MouseState GetState()
    {
        return Mouse.GetState();
    }

    public void Update()
    {
        MouseState currentState = Mouse.GetState();

        // 鼠标点击
        if (currentState.LeftButton == ButtonState.Pressed &&
            _previousState.LeftButton == ButtonState.Released)
        {
            _handler.HandleClick(currentState.Position);
            _isDragging = true;
        }

        // 鼠标松开
        if (currentState.LeftButton == ButtonState.Released &&
            _previousState.LeftButton == ButtonState.Pressed)
        {
            if (_isDragging)
            {
                _handler.PlacePlant(currentState.Position);
                _isDragging = false;
            }
        }

        _previousState = currentState;
    }
}