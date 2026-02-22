public class SelectPlantCommand : ICommand
{
    private readonly IPlayerActions _player;
    private readonly int _plantType;

    public SelectPlantCommand(IPlayerActions player, int plantType)
    {
        _player = player ?? throw new System.ArgumentNullException(nameof(player));
        _plantType = plantType;
    }

    public void Execute()
    {
        _player.SetSelectedPlant(_plantType);
    }
}
