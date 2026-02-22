using System.Drawing;

public interface ICollectable : IClickable, IDrawable, IUpdatable
{
    // The value/amount this collectable provides when collected
    int Value { get; }
    
    // Whether this collectable has been collected
    bool IsCollected { get; }
    
    // The position of the collectable
    Point Position { get; set; }
    
    // Lifetime remaining before the collectable disappears
    float LifetimeRemaining { get; }
    
    // Collect this item (returns the value)
    int Collect();
}