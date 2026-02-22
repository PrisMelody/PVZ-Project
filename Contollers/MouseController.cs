using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class MouseController : IMouse
{
    private MouseState previousSttate;
    private PVZ_Project.Game1 game;

    public MouseController(PVZ_Project.Game1 game)
    {
        this.game = game;
    }

    public MouseState GetState()
    {
        return Mouse.GetState();
    }

    public void Update()
    {
        MouseState currentState = Mouse.GetState();

       // Detect left-click (from release -> press)
        if (currentState.LeftButton == ButtonState.Pressed &&
            previousState.LeftButton == ButtonState.Released)
        {
            HandleClick(currentState.Position);
        }

        previousState = currentState;
    }

    private void HandleClick(Point mousePos)
    {
        // Click on the plant area → Select a plant
        if (game.IsInPlantMenu(mousePos))
        {
            int type = game.GetPlantType(mousePos);
            new SelectPlantCommand(game, type).Execute();
        }

        // Click the grass
        else if (game.IsOnGrid(mousePos))
        {
            
                new PlacePlantCommand(game, mousePos).Execute();
            
        }
    }
}