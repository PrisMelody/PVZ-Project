using System.Drawing;
using Microsoft.Xna.Framework.Graphics;

public class SunCollectable : ICollectable
{
    public int Value { get; private set; }
    public bool IsCollected { get; private set; }
    public Point Position { get; set; }
    public float LifetimeRemaining { get; private set; }
    public Rectangle Bounds { get; private set; }
    public int DrawOrder { get; set; }

    private float lifetime;
    private float fallSpeed;
    private bool isFalling;

    public SunCollectable(Point position, int value, float lifetime, Rectangle bounds)
    {
        Position = position;
        Value = value;
        this.lifetime = lifetime;
        LifetimeRemaining = lifetime;
        Bounds = bounds;
        IsCollected = false;
        DrawOrder = 50; // Medium draw order
        isFalling = true;
        fallSpeed = 50f; // pixels per second
    }

    public void Draw(SpriteBatch sprite)
    {
        if (!IsCollected)
        {
            // TODO: Draw sun sprite at Position
            // This will be implemented when sprites are available
        }
    }

    public void Update(Gametime gameTime)
    {
        if (IsCollected) return;

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Handle falling animation
        if (isFalling)
        {
            Position = new Point(Position.X, Position.Y + (int)(fallSpeed * deltaTime));
            Bounds = new Rectangle(Position.X - Bounds.Width / 2,
                                  Position.Y - Bounds.Height / 2,
                                  Bounds.Width, Bounds.Height);

            // Stop falling after a short time or when reaching ground
            // TODO: Add ground collision check
            isFalling = false; // Simplified for now
        }

        // Update lifetime
        LifetimeRemaining -= deltaTime;
        if (LifetimeRemaining <= 0)
        {
            // Sun disappears after lifetime expires
            IsCollected = true;
        }
    }

    public int Collect()
    {
        if (!IsCollected)
        {
            IsCollected = true;
            return Value;
        }
        return 0;
    }
}
