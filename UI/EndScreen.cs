using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class EndScreen
{
    private readonly Texture2D _pixel;
    private readonly SpriteFont _font;

    private const int ScreenWidth = 800;
    private const int ScreenHeight = 600;
    private const float TitleScale = 3.5f;
    private const float SubScale = 1.6f;

    public EndScreen(Texture2D pixel, SpriteFont font)
    {
        _pixel = pixel;
        _font = font;
    }

    public void Draw(SpriteBatch spriteBatch, GameStatus status)
    {
        // Semi-transparent dark overlay
        spriteBatch.Draw(_pixel, new Rectangle(0, 0, ScreenWidth, ScreenHeight), new Color(0, 0, 0, 170));

        bool won = status == GameStatus.Won;
        string title = won ? "VICTORY!" : "GAME OVER";
        Color titleColor = won ? new Color(255, 215, 0) : new Color(220, 50, 50);

        // Title with drop shadow
        var titleSize = _font.MeasureString(title) * TitleScale;
        var titlePos = new Vector2((ScreenWidth - titleSize.X) / 2f, ScreenHeight / 2f - 90f);
        spriteBatch.DrawString(_font, title, titlePos + new Vector2(4, 4), Color.Black,
            0f, Vector2.Zero, TitleScale, SpriteEffects.None, 0f);
        spriteBatch.DrawString(_font, title, titlePos, titleColor,
            0f, Vector2.Zero, TitleScale, SpriteEffects.None, 0f);

        // Subtitle
        string sub = "Press  R  to Restart";
        var subSize = _font.MeasureString(sub) * SubScale;
        var subPos = new Vector2((ScreenWidth - subSize.X) / 2f, ScreenHeight / 2f + 30f);
        spriteBatch.DrawString(_font, sub, subPos + new Vector2(2, 2), Color.Black,
            0f, Vector2.Zero, SubScale, SpriteEffects.None, 0f);
        spriteBatch.DrawString(_font, sub, subPos, Color.White,
            0f, Vector2.Zero, SubScale, SpriteEffects.None, 0f);
    }
}
