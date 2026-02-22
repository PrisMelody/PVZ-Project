using Microsoft.Xna.Framework;
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
    public int DrawOrder { get; set; }

    private int maxHealth;
    private Vector2 startPosition;
    private int screenWidth;

    public Lawnmower(Vector2 position, int row, int screenWidth)
    {
        Position = position;
        startPosition = position;
        Row = row;
        IsActivated = false;
        IsMoving = false;
        Speed = 300f;
        maxHealth = 1;
        Health = maxHealth;
        IsDead = false;
        DrawOrder = 20;
        this.screenWidth = screenWidth;
    }

    public void Draw(SpriteBatch sprite)
    {
        if (!IsDead)
        {
            // TODO: Draw lawnmower sprite at Position
        }
    }

    public void Update(GameTime gameTime)
    {
        if (IsDead) return;

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (IsActivated && IsMoving)
        {
            Position = new Vector2(Position.X + Speed * deltaTime, Position.Y);

            if (Position.X > screenWidth)
            {
                IsMoving = false;
            }
        }
    }

    public void Activate()
    {
        if (!IsActivated)
        {
            IsActivated = true;
            IsMoving = true;
        }
    }

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
