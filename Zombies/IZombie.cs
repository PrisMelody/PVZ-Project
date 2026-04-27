using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


    public interface IZombie : IDamageable, IPvZDrawable, IPvZUpdatable
    {
        float Speed { get; set; }
        bool IsAttacking { get; set; }
        public float MaxRange {get;}
        public float MinRange {get;}


        float xCoord { get; set; }
        float yCoord { get; set; }

        new int Health { get; set; }
        new bool IsDead { get; set; }
        new int DrawOrder { get; set; }
        int Damage {get; set;}
        Color DrawColor {get; set; }
        int SpawnWaveIndex {get; set; }
        int Lane {get;}
        void Move();
        void Attack();
}
