using Microsoft.Xna.Framework;

public interface ICollectable : IClickable, IPvZDrawable, IPvZUpdatable
{
    int Value { get; }
    bool IsCollected { get; }
    Point Position { get; set; }
    float LifetimeRemaining { get; }

    int Collect();
}
