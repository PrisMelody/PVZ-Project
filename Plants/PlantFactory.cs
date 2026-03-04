using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprites;

public class PlantFactory : IPlantFactory
{
    private readonly Dictionary<PlantType, Texture2D> _spriteSheets = new();

    private const int SpriteSheetCols = 12;
    private const int SpriteSheetRows = 2;
    private const int FrameWidth = 80;
    private const int FrameHeight = 80;
    private const float FrameTime = 0.05f;

    private static readonly Dictionary<PlantType, string> SheetNames = new()
    {
        { PlantType.Peashooter, "peashooter" },
        { PlantType.Sunflower, "sunflower" },
        { PlantType.SnowPea, "snowpea" },
        { PlantType.Repeater, "repeater" },
    };

    public void LoadContent(ContentManager content)
    {
        foreach (var (type, name) in SheetNames)
            _spriteSheets[type] = content.Load<Texture2D>(name);
    }

    public IPlant Create(PlantType type, float x, float y)
    {
        AnimatedSprite sprite = null;
        if (_spriteSheets.TryGetValue(type, out var sheet))
        {
            var anim = CreateSpriteSheetAnimation(sheet);
            sprite = new AnimatedSprite(anim, new Vector2(x, y));
        }

        return type switch
        {
            PlantType.Peashooter => new Peashooter(sprite, x, y),
            PlantType.Sunflower => new Sunflower(sprite, x, y),
            PlantType.SnowPea => new SnowPea(sprite, x, y),
            PlantType.Repeater => new Repeater(sprite, x, y),
            PlantType.CherryBomb => new CherryBomb(sprite, x, y),
            PlantType.WallNut => new WallNut(sprite, x, y),
            PlantType.PotatoMine => new PotatoMine(sprite, x, y),
            PlantType.Chomper => new Chomper(sprite, x, y),
            _ => null
        };
    }

    private static Animation CreateSpriteSheetAnimation(Texture2D sheet)
    {
        var frames = new List<ITextureRegion>();
        for (int r = 0; r < SpriteSheetRows; r++)
            for (int c = 0; c < SpriteSheetCols; c++)
                frames.Add(new TextureRegion(
                    $"frame_{r * SpriteSheetCols + c}",
                    sheet,
                    new Rectangle(c * FrameWidth, r * FrameHeight, FrameWidth, FrameHeight)));
        return new Animation(frames, FrameTime);
    }
}
