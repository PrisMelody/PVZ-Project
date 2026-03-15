using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlantsVsZombies.Zombies
{
    public class Zombie : IZombie
    {
        // IZombie
        public float Speed { get; set; } = 1.0f;
        public bool IsAttacking { get; set; }

        public float xCoord { get; set; }
        public float yCoord { get; set; }

        // IDamageable
        public int Health { get; set; } = 100;

        public bool IsDead { get; set; }

        public void TakeDamage(int amount)
        {
            Health -= amount;

            if (Health <= 0)
            {
                IsDead = true;
            }
        }

        // Hitbox
        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)xCoord, (int)yCoord, 80, 80);
            }
        }

        // Movement
        public void Move()
        {
            xCoord -= Speed;
        }

        public void Attack()
        {
            IsAttacking = true;
        }

        // IPvZDrawable
        public int DrawOrder { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            // drawing later
        }

        // IPvZUpdatable
        public void Update(GameTime gameTime)
        {
            Move();
        }
    }
}