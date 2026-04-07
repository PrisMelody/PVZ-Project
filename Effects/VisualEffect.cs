using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace PVZ_Project.GameObjects 
{
    public class VisualEffect : IPvZUpdatable, IPvZDrawable
    {
        private Texture2D _texture; 
        private Vector2 _position;
        private float _timer;
        private float _lifeTime;
        
        public bool IsActive { get; private set; } = true;
        public int DrawOrder { get; set; }

        public VisualEffect(Texture2D texture, Vector2 position, float lifeTime)
        {
            _texture = texture;
            _position = position;
            _lifeTime = lifeTime;
            DrawOrder = 100;
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _timer += delta;

            if (_timer >= _lifeTime)
            {
                IsActive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                spriteBatch.Draw(_texture, _position, Color.White);
            }
        }
    }
}
