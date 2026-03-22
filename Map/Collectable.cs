using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Shared sun/coin pickup: falls at constant speed to a target Y, then rests until collected or idle time expires.
/// </summary>
public class Collectable : ICollectable
{
    private readonly CollectableKind _kind;
    private readonly Texture2D _texture;
    private readonly int _value;

    private float _x;
    private float _y;
    private readonly float _targetY;
    /// <summary>Pixels per second downward while falling; zero means no movement (should not happen).</summary>
    private readonly float _fallSpeed;

    private CollectablePhase _phase;
    private readonly float _idleDurationSeconds;
    private float _idleTimeRemaining;
    private bool _gone;

    public const int DefaultSunValue = 25;
    public const int DefaultCoinValue = 1;

    /// <summary>Sky sun: slow linear fall (PvZ-like).</summary>
    private const float SkySunFallSpeed = 72f;

    /// <summary>Coin drop: short linear fall from above the zombie.</summary>
    private const float CoinFallSpeed = 140f;

    private enum CollectablePhase
    {
        Falling,
        Idle
    }

    public CollectableKind Kind => _kind;
    public int Value => _value;
    public bool IsCollected => _gone;
    public float LifetimeRemaining => _phase == CollectablePhase.Idle ? _idleTimeRemaining : 0f;

    public Point Position
    {
        get => new Point((int)Math.Round(_x), (int)Math.Round(_y));
        set
        {
            _x = value.X;
            _y = value.Y;
        }
    }

    public Rectangle Bounds { get; private set; }
    public int DrawOrder { get; set; }

    /// <summary>
    /// Sky sun: starts above screen, falls linearly to <paramref name="targetY"/>.
    /// </summary>
    public static Collectable CreateSkySun(
        Texture2D texture,
        float startX,
        float startY,
        float targetY,
        float idleDurationSeconds)
    {
        return new Collectable(
            CollectableKind.Sun,
            texture,
            DefaultSunValue,
            startX,
            startY,
            targetY,
            SkySunFallSpeed,
            idleDurationSeconds);
    }

    /// <summary>
    /// Zombie coin drop: short linear fall from slightly above spawn point.
    /// </summary>
    public static Collectable CreateCoinDrop(
        Texture2D texture,
        float worldX,
        float worldY,
        float idleDurationSeconds)
    {
        float dropHeight = 28f;
        float targetY = worldY;
        float startY = worldY - dropHeight;
        return new Collectable(
            CollectableKind.Coin,
            texture,
            DefaultCoinValue,
            worldX,
            startY,
            targetY,
            CoinFallSpeed,
            idleDurationSeconds);
    }

    private Collectable(
        CollectableKind kind,
        Texture2D texture,
        int value,
        float startX,
        float startY,
        float targetY,
        float fallSpeedPixelsPerSecond,
        float idleDurationSeconds)
    {
        _kind = kind;
        _texture = texture;
        _value = value;
        _x = startX;
        _y = startY;
        _targetY = targetY;
        _fallSpeed = fallSpeedPixelsPerSecond;
        _phase = CollectablePhase.Falling;
        _idleDurationSeconds = idleDurationSeconds;
        _idleTimeRemaining = 0f;
        DrawOrder = 50;
        UpdateBounds();
    }

    public void Update(GameTime gameTime)
    {
        if (_gone)
            return;

        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_phase == CollectablePhase.Falling)
        {
            _y += _fallSpeed * dt;

            if (_y >= _targetY)
            {
                _y = _targetY;
                _phase = CollectablePhase.Idle;
                _idleTimeRemaining = _idleDurationSeconds;
            }
        }
        else
        {
            _idleTimeRemaining -= dt;
            if (_idleTimeRemaining <= 0f)
                _gone = true;
        }

        UpdateBounds();
    }

    private void UpdateBounds()
    {
        int w = _texture.Width;
        int h = _texture.Height;
        int drawX = (int)Math.Round(_x - w / 2f);
        int drawY = (int)Math.Round(_y - h / 2f);
        Bounds = new Rectangle(drawX, drawY, w, h);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (_gone)
            return;

        var pos = new Vector2(
            (float)Math.Round(_x - _texture.Width / 2f),
            (float)Math.Round(_y - _texture.Height / 2f));
        var tint = _kind == CollectableKind.Coin ? new Color(255, 210, 64) : Color.White;
        spriteBatch.Draw(_texture, pos, tint);
    }

    public bool HitTest(Point mousePos) => !_gone && Bounds.Contains(mousePos);

    public void OnClick(IMouse mouse)
    {
        Collect();
    }

    public int Collect()
    {
        if (_gone)
            return 0;
        _gone = true;
        return _value;
    }
}
