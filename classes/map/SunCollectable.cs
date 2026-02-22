using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SunCollectable : ICollectable
{
    public int Value { get; private set; }
    public bool IsCollected { get; private set; }
    public System.Drawing.Point Position { get; set; }
    public float LifetimeRemaining { get; private set; }
    public System.Drawing.Rectangle Bounds { get; private set; }
    public int DrawOrder { get; set; }

    private float lifetime;
    private float fallSpeed;
    private bool isFalling;

    public SunCollectable(System.Drawing.Point position, int value, float lifetime, System.Drawing.Rectangle bounds)
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

    public bool HitTest(System.Drawing.Point mousePos)
    {
        return Bounds.Contains(mousePos);
    }

    public void OnClick(MouseController mouse)
    {
        Collect();
    }

    public void Draw(SpriteBatch sprite)
    {
        if (!IsCollected)
        {
            // TODO: Draw sun sprite at Position
        }
    }

    public void Update(GameTime gameTime)
    {
        if (IsCollected) return;

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (isFalling)
        {
            Position = new System.Drawing.Point(Position.X, Position.Y + (int)(fallSpeed * deltaTime));
            Bounds = new System.Drawing.Rectangle(
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
