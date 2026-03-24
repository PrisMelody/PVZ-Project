using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



    public class SunflowerStateManager
    {
        private readonly Sunflower _plant;
        private double _timeSinceLastSun = 0;
        private const double SunInterval = 3.0;
    private readonly Texture2D _pixel;

    public bool ShowSunThisFrame { get; private set; } = false;

    public SunflowerStateManager(Sunflower plant, Texture2D pixel)
    {
        _plant = plant;
        _pixel = pixel;
    }

    public void Update(GameTime gameTime)
    {
        ShowSunThisFrame = false;

        _timeSinceLastSun += gameTime.ElapsedGameTime.TotalSeconds;
        if (_timeSinceLastSun >= SunInterval)
        {
            _timeSinceLastSun = 0;
            ShowSunThisFrame = true;
            System.Console.WriteLine($"Sun produced by sunflower at ({_plant.XPos}, {_plant.YPos})");
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (ShowSunThisFrame)
        {
            spriteBatch.Begin();
            Rectangle sunRect = new Rectangle((int)_plant.XPos, (int)_plant.YPos - 20, 20, 20);
            spriteBatch.Draw(_pixel, sunRect, Color.Blue);
            spriteBatch.End();
        }
    }
}
