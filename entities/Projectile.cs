using Microsoft.Xna.Framework;

namespace PlantsVsZombies.entities
{
    public class Projectile
    {
        public Vector2 Position;

        public int Width = 20;
        public int Height = 20;

        public int Damage = 20;

        public bool IsActive { get; set; } = true;

        public Rectangle Hitbox
{
    get
    {
        return new Rectangle(
            (int)Position.X,
            (int)Position.Y,
            Width,
            Height
        );
    }
}
    }
}