public class SelectPlantCommand : ICommand
{
    private Game1 game;
    private int plantType;

    public SelectPlantCommand(Game1 game, int type)
    {
        this.game = game;
        plantType = type;
    }

    public void Execute()
    {
        game.SelectedPlant = plantType;
    }
}