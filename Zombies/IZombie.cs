using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


    public interface IZombie : IDamageable, IPvZDrawable, IPvZUpdatable
    {
        float Speed { get; set; }
        bool IsAttacking { get; set; }

        float xCoord { get; set; }
        float yCoord { get; set; }

        new int Health { get; set; }
        new bool IsDead { get; set; }
        new int DrawOrder { get; set; }
        int SpawnWaveIndex {get; set; }

        void Move();
        void Attack();
    }

