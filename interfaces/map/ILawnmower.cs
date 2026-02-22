using System.Drawing;

public interface ILawnmower : IDrawable, IUpdatable, IDamageable
{
    // The position of the lawnmower
    Vector2 Position { get; set; }

    // The row this lawnmower is in (0-4)
    int Row { get; }

    // Whether the lawnmower has been activated/triggered
    bool IsActivated { get; }

    // Whether the lawnmower is currently moving
    bool IsMoving { get; }

    // Speed at which the lawnmower moves
    float Speed { get; }

    // Activate the lawnmower (when zombie reaches it)
    void Activate();

    // Check collision with zombies
    //void CheckCollision();
}