using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Spawns sky suns on a timer, accepts zombie coin spawns, updates/draws all pickups.
/// </summary>
public class CollectableManager : ICollectableSpawner
{
    private readonly List<Collectable> _collectables = new();
    private readonly Texture2D _sunTexture;
    private readonly Texture2D _coinTexture;
    private readonly Random _random = new();

    private float _skySunTimer;

    /// <summary>Seconds between natural sky sun spawns (PvZ-like ~7.5–8s).</summary>
    public float SkySunIntervalSeconds { get; set; } = 7.5f;

    /// <summary>Time pickups bob on the ground before disappearing if not collected.</summary>
    public float IdleLifetimeSeconds { get; set; } = 12f;

    /// <summary>Probability [0,1] that a dead zombie drops a coin.</summary>
    public float CoinDropChance { get; set; } = 0.4f;

    // Match Map / lawn layout
    public const int GridRows = 5;
    public const int GridCols = 9;
    public const int CellWidth = 80;
    public const int CellHeight = 90;
    public const int GridOriginX = 40;
    public const int GridOriginY = 100;

    public CollectableManager(Texture2D sunTexture, Texture2D coinTexture)
    {
        _sunTexture = sunTexture ?? throw new ArgumentNullException(nameof(sunTexture));
        _coinTexture = coinTexture ?? throw new ArgumentNullException(nameof(coinTexture));
    }

    public void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _skySunTimer += dt;
        if (_skySunTimer >= SkySunIntervalSeconds)
        {
            _skySunTimer = 0f;
            SpawnSkySun();
        }

        for (int i = 0; i < _collectables.Count; i++)
            _collectables[i].Update(gameTime);

        _collectables.RemoveAll(c => c.IsCollected);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var c in _collectables)
            c.Draw(spriteBatch);
    }

    /// <summary>Topmost pickup under the cursor (last drawn wins).</summary>
    public CollectPickupResult TryCollectAt(Point screenPosition)
    {
        for (int i = _collectables.Count - 1; i >= 0; i--)
        {
            var c = _collectables[i];
            if (!c.HitTest(screenPosition))
                continue;

            int value = c.Collect();
            if (value > 0)
                return new CollectPickupResult(true, c.Kind, value);
        }

        return new CollectPickupResult(false, default, 0);
    }

    private void SpawnSkySun()
    {
        int margin = 8;
        float lawnLeft = GridOriginX + margin;
        float lawnRight = GridOriginX + GridCols * CellWidth - margin;
        float x = lawnLeft + (float)_random.NextDouble() * (lawnRight - lawnLeft);
        float startY = -40f;

        int row = _random.Next(0, GridRows);
        float targetY = GridOriginY + row * CellHeight + CellHeight * 0.55f;

        var sun = Collectable.CreateSkySun(_sunTexture, x, startY, targetY, IdleLifetimeSeconds);
        _collectables.Add(sun);
    }

    public void SpawnCoinAt(Vector2 position)
    {
        if (_random.NextDouble() > CoinDropChance)
            return;

        var coin = Collectable.CreateCoinDrop(_coinTexture, position.X, position.Y, IdleLifetimeSeconds);
        _collectables.Add(coin);
    }
}
