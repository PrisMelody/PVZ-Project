using Microsoft.Xna.Framework;

namespace PlantsVsZombies.entities
{
    public class GameObject
    {
        public Vector2 Position;
        public int Width;
        public int Height;

        public bool IsActive = true;

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