using Microsoft.Xna.Framework;

public class PlacePlantCommand : ICommand
{
    private Game1 game;
    private Point position;

    public PlacePlantCommand(Game1 game, Point pos)
    {
        this.game = game;
        position = pos;
    }

    public void Execute()
    {
        game.PlacePlant(position);
    }
}