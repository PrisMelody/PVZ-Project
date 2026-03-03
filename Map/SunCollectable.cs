using Microsoft.Xna.Framework;
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
        DrawOrder = 50;
        isFalling = true;
        fallSpeed = 50f;
    }

    public bool HitTest(Point mousePos)
    {
        return Bounds.Contains(mousePos);
    }

    public void OnClick(IMouse mouse)
    {
        Collect();
    }

    public void Draw(SpriteBatch sprite)
    {
        if (!IsCollected)
        {
        }
    }

    public void Update(GameTime gameTime)
    {
        if (IsCollected) return;

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (isFalling)
        {
            Position = new Point(Position.X, Position.Y + (int)(fallSpeed * deltaTime));
            Bounds = new Rectangle(
                Position.X - Bounds.Width / 2,
                Position.Y - Bounds.Height / 2,
                Bounds.Width, Bounds.Height);
            isFalling = false;
        }

        LifetimeRemaining -= deltaTime;
        if (LifetimeRemaining <= 0)
        {
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
