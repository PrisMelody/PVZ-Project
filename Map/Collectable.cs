using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Shared sun/coin pickup: falls to a target Y, then bobs until collected or idle time expires.
/// </summary>
public class Collectable : ICollectable
{
    private readonly CollectableKind _kind;
    private readonly Texture2D _texture;
    private readonly int _value;

    private float _x;
    private float _y;
    private readonly float _targetY;
    private float _fallVelocity;
    private readonly float _fallAcceleration;

    private CollectablePhase _phase;
    private readonly float _idleDurationSeconds;
    private float _idleTimeRemaining;
    private float _bobPhase;
    private bool _gone;

    public const int DefaultSunValue = 25;
    public const int DefaultCoinValue = 1;

    private const float BobSpeed = 6f;
    private const float BobAmplitude = 4f;

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
    /// Sky sun: starts above screen, falls to <paramref name="targetY"/>.
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
            initialFallVelocity: 120f,
            fallAcceleration: 420f,
            idleDurationSeconds);
    }

    /// <summary>
    /// Zombie coin drop: short fall from slightly above spawn point.
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
            initialFallVelocity: 0f,
            fallAcceleration: 980f,
            idleDurationSeconds);
    }

    private Collectable(
        CollectableKind kind,
        Texture2D texture,
        int value,
        float startX,
        float startY,
        float targetY,
        float initialFallVelocity,
        float fallAcceleration,
        float idleDurationSeconds)
    {
        _kind = kind;
        _texture = texture;
        _value = value;
        _x = startX;
        _y = startY;
        _targetY = targetY;
        _fallVelocity = initialFallVelocity;
        _fallAcceleration = fallAcceleration;
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
            _fallVelocity += _fallAcceleration * dt;
            _y += _fallVelocity * dt;

            if (_y >= _targetY)
            {
                _y = _targetY;
                _phase = CollectablePhase.Idle;
                _bobPhase = 0f;
                _idleTimeRemaining = _idleDurationSeconds;
            }
        }
        else
        {
            _bobPhase += dt * BobSpeed;
            _idleTimeRemaining -= dt;
            if (_idleTimeRemaining <= 0f)
                _gone = true;
        }

        UpdateBounds();
    }

    private void UpdateBounds()
    {
        float bobY = _phase == CollectablePhase.Idle
            ? (float)(Math.Sin(_bobPhase) * BobAmplitude)
            : 0f;

        int w = _texture.Width;
        int h = _texture.Height;
        int drawX = (int)Math.Round(_x - w / 2f);
        int drawY = (int)Math.Round(_y - h / 2f + bobY);
        Bounds = new Rectangle(drawX, drawY, w, h);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (_gone)
            return;

        float bobY = _phase == CollectablePhase.Idle
            ? (float)(Math.Sin(_bobPhase) * BobAmplitude)
            : 0f;

        var pos = new Vector2(
            (float)Math.Round(_x - _texture.Width / 2f),
            (float)Math.Round(_y - _texture.Height / 2f + bobY));
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
