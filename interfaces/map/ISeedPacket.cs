using System.Drawing;

public interface ISeedPacket : IClickable, IPvZDrawable, IDraggable
{
    // The type of plant this seed packet represents
    string PlantType { get; }

    // The cost in sun to use this seed packet
    int Cost { get; }

    // Whether this seed packet is available (enough sun, cooldown ready)
    bool IsAvailable { get; }

    // Whether this seed packet is on cooldown
    bool IsOnCooldown { get; }

    // Cooldown time remaining
    float CooldownRemaining { get; }

    // Check if this seed packet can be planted on a plot
    bool CanPlantOn(IGridPlot plot);

    // Plant the seed on a plot
    void PlantOn(IGridPlot plot);
}
