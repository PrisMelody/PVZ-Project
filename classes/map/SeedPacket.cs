using System.Drawing;
using Microsoft.Xna.Framework.Graphics;

public class SeedPacket : ISeedPacket
{
    public string PlantType { get; private set; }
    public int Cost { get; private set; }
    public bool IsAvailable { get; private set; }
    public bool IsOnCooldown { get; private set; }
    public float CooldownRemaining { get; private set; }
    public Rectangle Bounds { get; private set; }
    public bool IsDragging { get; private set; }
    public int DrawOrder { get; set; }

    private Point position;
    private float cooldownTime;
    private int currentSun;

    public SeedPacket(string plantType, int cost, float cooldownTime, Point position, Rectangle bounds)
    {
        PlantType = plantType;
        Cost = cost;
        this.cooldownTime = cooldownTime;
        this.position = position;
        Bounds = bounds;
        CooldownRemaining = 0f;
        IsOnCooldown = false;
        IsDragging = false;
        DrawOrder = 100; // High draw order for UI elements
        UpdateAvailability(0); // Initialize with 0 sun
    }

    public void Draw(SpriteBatch sprite)
    {
        // TODO: Draw seed packet sprite
        // Draw with reduced opacity if on cooldown or unavailable
        // This will be implemented when sprites are available
    }

    public void OnDragStart(InputState input)
    {
        if (IsAvailable && !IsOnCooldown)
        {
            IsDragging = true;
        }
    }

    public void OnDrag(InputState input)
    {
        if (IsDragging)
        {
            // Update position to follow mouse
            Bounds = new Rectangle(input.MousePosition.X - Bounds.Width / 2,
                                  input.MousePosition.Y - Bounds.Height / 2,
                                  Bounds.Width, Bounds.Height);
        }
    }

    public void OnDragEnd(InputState input)
    {
        IsDragging = false;
    }

    public bool CanPlantOn(IGridPlot plot)
    {
        return plot != null && !plot.IsOccupied && plot.CanPlacePlant;
    }

    public void PlantOn(IGridPlot plot)
    {
        if (CanPlantOn(plot) && IsAvailable && !IsOnCooldown)
        {
            // TODO: Create plant instance based on PlantType and place on plot
            // This will be implemented when plant classes are available
            // For now, just start cooldown
            StartCooldown();
        }
    }

    public void UpdateAvailability(int sunAmount)
    {
        currentSun = sunAmount;
        IsAvailable = currentSun >= Cost && !IsOnCooldown;
    }

    public void StartCooldown()
    {
        IsOnCooldown = true;
        CooldownRemaining = cooldownTime;
    }

    public void UpdateCooldown(Gametime gameTime)
    {
        if (IsOnCooldown)
        {
            CooldownRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (CooldownRemaining <= 0)
            {
                CooldownRemaining = 0;
                IsOnCooldown = false;
                UpdateAvailability(currentSun);
            }
        }
    }
}
