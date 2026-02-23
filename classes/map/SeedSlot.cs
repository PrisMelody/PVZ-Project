using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SeedSlot
{
    private readonly Texture2D _trayTexture;
    private readonly Texture2D[] _packetTextures;
    private readonly Texture2D _pixel;
    private readonly Rectangle _trayBounds;
    private readonly Rectangle[] _packetBounds;

    private const int TrayWidth = 446;
    private const int TrayHeight = 87;
    private const int PacketWidth = 50;
    private const int PacketHeight = 70;
    private const int PacketMarginLeft = 85;
    private const int PacketGap = 5;

    public SeedSlot(Texture2D trayTexture, Texture2D[] packetTextures, Texture2D pixel, int x, int y)
    {
        _trayTexture = trayTexture;
        _packetTextures = packetTextures;
        _pixel = pixel;
        _trayBounds = new Rectangle(x, y, TrayWidth, TrayHeight);

        int packetY = y + (TrayHeight - PacketHeight) / 2;
        _packetBounds = new Rectangle[packetTextures.Length];
        int packetX = x + PacketMarginLeft;
        for (int i = 0; i < packetTextures.Length; i++)
        {
            _packetBounds[i] = new Rectangle(packetX, packetY, PacketWidth, PacketHeight);
            packetX += PacketWidth + PacketGap;
        }
    }

    public int HitTestPacket(int screenX, int screenY)
    {
        for (int i = 0; i < _packetBounds.Length; i++)
        {
            if (_packetBounds[i].Contains(screenX, screenY))
                return i;
        }
        return -1;
    }

    public Rectangle TrayBounds => _trayBounds;

    public void Draw(SpriteBatch spriteBatch, int selectedIndex)
    {
        spriteBatch.Draw(_trayTexture, _trayBounds, Color.White);

        for (int i = 0; i < _packetTextures.Length; i++)
            spriteBatch.Draw(_packetTextures[i], _packetBounds[i], Color.White);

        if (selectedIndex >= 0 && selectedIndex < _packetBounds.Length)
            spriteBatch.Draw(_pixel, _packetBounds[selectedIndex], new Color(255, 255, 255, 60));
    }
}
