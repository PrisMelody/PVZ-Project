using Microsoft.Xna.Framework;

public interface ILawnmower : IPvZDrawable, IPvZUpdatable, IDamageable
{
    Vector2 Position { get; set; }

    int Row { get; }

    bool IsActivated { get; }

    bool IsMoving { get; }

    float Speed { get; }

    void Activate();
}
