using System.Drawing;
using Microsoft.Xna.Framework.Graphics;

public class Lawnmower : ILawnmower
{
    public Vector2 Position { get; set; }
    public int Row { get; private set; }
    public bool IsActivated { get; private set; }
    public bool IsMoving { get; private set; }
    public float Speed { get; private set; }
    public int Health { get; private set; }
    public bool IsDead { get; private set; }
    private int maxHealth;
    private Vector2 startPosition;
    private int screenWidth;

    public Lawnmower(Point position, int row, Rectangle bounds, int screenWidth)
    {
        Position = position;
        startPosition = position;
        Row = row;
        IsActivated = false;
        IsMoving = false;
        Speed = 300f; // pixels per second
        maxHealth = 1;
        Health = maxHealth;
        IsDead = false;
        this.screenWidth = screenWidth;
    }

    public void Draw(SpriteBatch sprite)
    {
        if (!IsDead)
        {
            // TODO: Draw lawnmower sprite at Position
            // This will be implemented when sprites are available
        }
    }

    public void Update(Gametime gameTime)
    {
        if (IsDead) return;

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Move lawnmower if activated
        if (IsActivated && IsMoving)
        {
            int newX = Position.X + (int)(Speed * deltaTime);
            Position = new Point(newX, Position.Y);
            Bounds = new Rectangle(Position.X - Bounds.Width / 2,
                                  Position.Y - Bounds.Height / 2,
                                  Bounds.Width, Bounds.Height);

            // Check if lawnmower has moved off screen
            if (Position.X > screenWidth)
            {
                IsMoving = false;
                // Lawnmower is done, could be removed or reset
            }
        }

        // Check for collisions with zombies
        //CheckCollision();
    }

    public void Activate()
    {
        if (!IsActivated)
        {
            IsActivated = true;
            IsMoving = true;
        }
    }

    /*public void CheckCollision()
    {
        // TODO: Check collision with zombies in the same row
        // This will be implemented when zombie classes are available
        // For now, this is a placeholder
    }
    */
    public void TakeDamage(int amount)
    {
        if (!IsDead)
        {
            Health -= amount;
            if (Health <= 0)
            {
                Health = 0;
                IsDead = true;
                IsMoving = false;
            }
        }
    }
}
