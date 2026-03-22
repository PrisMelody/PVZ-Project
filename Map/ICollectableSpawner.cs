using Microsoft.Xna.Framework;

/// <summary>
/// Allows zombie code to spawn coin pickups without depending on <see cref="CollectableManager"/> concretely.
/// </summary>
public interface ICollectableSpawner
{
    void SpawnCoinAt(Vector2 position);
}
