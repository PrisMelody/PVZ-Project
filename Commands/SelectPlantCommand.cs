public class SelectPlantCommand : ICommand
{
    private readonly IPlayerActions _player;
    private readonly PlantType _plantType;

    public SelectPlantCommand(IPlayerActions player, PlantType plantType)
    {
        _player = player ?? throw new System.ArgumentNullException(nameof(player));
        _plantType = plantType;
    }

    public void Execute()
    {
        _player.SetSelectedPlant(_plantType);
    }
}
