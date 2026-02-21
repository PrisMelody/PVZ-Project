using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace PVZ_Project.GameObjects 
{
    // 继承 IDrawable 和 IUpdatable，它们需要被画出来，也需要随时间播放动画
    public class VisualEffect : IPvZUpdatable, IPvZDrawable
    {
        private Texture2D _texture; 
        private Vector2 _position;
        private float _timer;
        private float _lifeTime; // 特效持续时间
        
        public bool IsActive { get; private set; } = true; // 标记是否还存活
        public int DrawOrder { get; set; } // 允许外部设置层级

        public VisualEffect(Texture2D texture, Vector2 position, float lifeTime)
        {
            _texture = texture;
            _position = position;
            _lifeTime = lifeTime;
            DrawOrder = 100; // 默认层级
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _timer += delta;

            // 逻辑
            
            // 清理
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