public readonly struct CollectPickupResult
{
    public CollectPickupResult(bool collected, CollectableKind kind, int value)
    {
        Collected = collected;
        Kind = kind;
        Value = value;
    }

    public bool Collected { get; }
    public CollectableKind Kind { get; }
    public int Value { get; }
}
