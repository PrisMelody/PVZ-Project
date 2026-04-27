using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BasicZombie : IZombie
{
    
    readonly protected static Dictionary<int, float> LaneSpawnLocations = new Dictionary<int, float>(){
            { 0, 20.0f }, //Mostly correct value
            { 1, 120.0f }, 
            { 2, 225.0f },
            { 3, 305.0f },
            { 4, 415.0f },
        };
    private readonly ITextureRegion _region;
    private readonly float _scale;

    public float Range {get;} = 100;
    public bool IsAttacking { get; set; } = false;
    public float Speed { get; set; } = 0.25f;
    public float xCoord { get; set; } = 900.0f;
    public float yCoord { get; set; }
    public int Health { get; set; } = 270;
    public bool IsDead { get; set; }
    public int DrawOrder { get; set; }
    public int SpawnWaveIndex { get; set; }

    public int Lane {get;}

    public BasicZombie(ITextureRegion region, float scale, int lane) //TODO: Scale probably doesn't need to be an input.
    {
        _region = region;
        _scale = scale;
        yCoord = LaneSpawnLocations[lane];
        Lane = lane;
    }

    public void Move()
    {
        xCoord -= Speed;
    }

    public void Attack()
    {
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            IsDead = true;
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch) //TODO: replace Sprites for zombies with the updated plant sprites.
    {
        spriteBatch.Draw(
            _region.Texture,
            new Vector2(xCoord, yCoord),
            _region.SourceRectangle,
            Color.White,
            0.0f,
            Vector2.Zero,
            _scale,
            SpriteEffects.None,
            0.0f
        );
    }

    public void Update(GameTime gameTime)
    {
        if (!IsAttacking)
        {
            Move();
        }
        else
        {
            Attack();
            IsAttacking = false;
        }
    }
}
