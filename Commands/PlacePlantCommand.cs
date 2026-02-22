using Microsoft.Xna.Framework;

public class PlacePlantCommand : ICommand
{
    private readonly IPlayerActions _player;
    private readonly Point _position;

    public PlacePlantCommand(IPlayerActions player, Point position)
    {
        _player = player ?? throw new System.ArgumentNullException(nameof(player));
        _position = position;
    }

    public void Execute()
    {
        _player.PlacePlant(_position);
    }
}
