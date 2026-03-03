public interface ISeedPacket : IClickable, IPvZDrawable, IDraggable
{
    string PlantType { get; }
    int Cost { get; }
    bool IsAvailable { get; }
    bool IsOnCooldown { get; }
    float CooldownRemaining { get; }

    bool CanPlantOn(IGridPlot plot);
    void PlantOn(IGridPlot plot);
}
