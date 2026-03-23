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
    private List<Projectile> _projectiles;
    private Texture2D _peaTexture;
    private Texture2D _snowPeaTexture;

    private static readonly Dictionary<PlantType, string> SheetNames = new()
    {
        { PlantType.Peashooter, "peashooter" },
        { PlantType.Sunflower, "sunflower" },
        { PlantType.SnowPea, "snowpea" },
        { PlantType.Repeater, "repeater" },
    };
    public PlantFactory(List<Projectile> projectiles, Texture2D peaTexture, Texture2D snowPeaTexture)
    {
        _projectiles = projectiles;
        _peaTexture = peaTexture;
        _snowPeaTexture = snowPeaTexture;
    }

    public void LoadContent(ContentManager content)
    {
        foreach (var (type, name) in SheetNames)
        {
            _spriteSheets[type] = content.Load<Texture2D>(name);
        }
        
    }

    public IPlant Create(PlantType type, float x, float y)
    {
        if (!_spriteSheets.TryGetValue(type, out var sheet))
            return null;

        //  Create ONE full animation using all frames
        var anim = CreateSpriteSheetAnimation(sheet);

        //  Pass animation into plant (no AnimatedSprite here anymore)
        return type switch
        {
            PlantType.Peashooter => new Peashooter(anim, anim, x, y,_projectiles, _peaTexture),
            PlantType.Sunflower => new Sunflower(anim, anim, x, y),
            PlantType.SnowPea => new SnowPea(anim, anim, x, y,_projectiles,_snowPeaTexture ),
            PlantType.Repeater => new Repeater(anim, anim, x, y,_projectiles, _peaTexture),
            _ => null
        };
    }

    
    private static Animation CreateSpriteSheetAnimation(Texture2D sheet)
    {
        var frames = new List<ITextureRegion>();

        for (int r = 0; r < SpriteSheetRows; r++)
        {
            for (int c = 0; c < SpriteSheetCols; c++)
            {
                frames.Add(new TextureRegion(
                    $"frame_{r * SpriteSheetCols + c}",
                    sheet,
                    new Rectangle(
                        c * FrameWidth,
                        r * FrameHeight,
                        FrameWidth,
                        FrameHeight
                    )
                ));
            }
        }

        return new Animation(frames, FrameTime);
    }
}