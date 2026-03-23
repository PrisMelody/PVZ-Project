using Microsoft.Xna.Framework;

namespace PVZ_Project
{
    public class Camera
    {
        // The X and Y position of the camera
        public Vector2 Position { get; set; }

        public Camera()
        {
            Position = Vector2.Zero;
        }

        // This shifts all drawing in the opposite direction of the camera
        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0));
        }

        // Converts screen mouse clicks into game-world coordinates
        public Vector2 ScreenToWorldSpace(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(GetViewMatrix()));
        }
    }
}