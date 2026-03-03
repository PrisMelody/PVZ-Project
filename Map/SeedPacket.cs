using Microsoft.Xna.Framework;
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
        DrawOrder = 100;
        UpdateAvailability(0);
    }

    public void Draw(SpriteBatch sprite)
    {
    }

    public bool HitTest(Point mousePos)
    {
        return Bounds.Contains(mousePos);
    }

    public void OnClick(IMouse mouse)
    {
    }

    public void OnDragStart(IMouse mouse)
    {
        if (IsAvailable && !IsOnCooldown)
        {
            IsDragging = true;
        }
    }

    public void OnDrag(IMouse mouse)
    {
        if (IsDragging)
        {
            var state = mouse.GetState();
            Bounds = new Rectangle(
                state.Position.X - Bounds.Width / 2,
                state.Position.Y - Bounds.Height / 2,
                Bounds.Width, Bounds.Height);
        }
    }

    public void OnDragEnd(IMouse mouse)
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

    public void UpdateCooldown(GameTime gameTime)
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
