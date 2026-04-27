using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprites;
using System;
using System.Linq;

public class PlantFactory : IPlantFactory
{
    private readonly Dictionary<PlantType, Texture2D> _spriteSheets = new();
    private readonly Dictionary<PlantType, TextureAtlas> _atlases = new();
    // some of the new plants dont have cleen disticnonts between rows and collumns,
    //  so we will just load the whole sheet as one animation,
    //  and use xml files to define the regions for the new plants, and load those into texture atlases

    private const int SpriteSheetCols = 12;
    private const int SpriteSheetRows = 2;
    private const int FrameWidth = 80;
    private const int FrameHeight = 80;
    private const float FrameTime = 0.05f;
    private List<Projectile> _projectiles;
    private Texture2D _peaTexture;
    private Texture2D _snowPeaTexture;

    private static readonly Dictionary<PlantType, float> AnimationSpeeds = new()
    {
        { PlantType.Peashooter, 1.2f },
        { PlantType.Sunflower, 1.0f },
        { PlantType.SnowPea, 1.2f },
        { PlantType.Repeater, 1.2f },
        { PlantType.Chomper, 0.7f }
    };

    private static readonly Dictionary<PlantType, string> SheetNames = new()
    {
        { PlantType.Peashooter, "peashooter" },
        { PlantType.Sunflower, "sunflower" },
        { PlantType.SnowPea, "snowpea" },
        { PlantType.Repeater, "repeater" },
    };
    private static readonly Dictionary<PlantType, string> AtlasNames = new()
    {
        { PlantType.Chomper, "chomper" },
        { PlantType.WallNut, "wallnut" },
        { PlantType.CherryBomb, "cherrybomb" }

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

        var loader = new XmlTextureAtlasLoader();

        foreach (var (type, name) in AtlasNames)
        {
            _atlases[type] = loader.Load(content, name);
        }
    }
   private (Animation idle, Animation attack) GetAnimations(PlantType type)
    {
        if (_atlases.TryGetValue(type, out var atlas))
        {
            return CreateAtlasAnimations(atlas);
        }

        if (_spriteSheets.TryGetValue(type, out var sheet))
        {
            var anim = CreateSpriteSheetAnimation(sheet);
            return (anim, anim); // fallback
        }

        return (null, null);
    }

    public IPlant Create(PlantType type, float x, float y)
{
    var (idleAnim, attackAnim) = GetAnimations(type);
    if (idleAnim == null || attackAnim == null)
        return null;

    return type switch
    {
        PlantType.Peashooter => new Peashooter(idleAnim, attackAnim, x, y, _projectiles, _peaTexture),
        PlantType.Sunflower => new Sunflower(idleAnim, attackAnim, x, y),
        PlantType.SnowPea => new SnowPea(idleAnim, attackAnim, x, y, _projectiles, _snowPeaTexture),
        PlantType.Repeater => new Repeater(idleAnim, attackAnim, x, y, _projectiles, _peaTexture),

        // NEW (atlas-based plants)
        PlantType.Chomper => new Chomper(idleAnim, attackAnim, x, y),
        PlantType.WallNut => new WallNut(idleAnim, attackAnim, x, y),
        PlantType.CherryBomb => new CherryBomb(idleAnim, attackAnim, x, y),

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
    private (Animation idle, Animation attack) CreateAtlasAnimations(TextureAtlas atlas)
    {
        var idleFrames = atlas.GetRegionNames()
            .Where(name => name.StartsWith("idle"))
            .OrderBy(name => int.Parse(name.Split('_')[1]))
            .Select(name => atlas.GetRegion(name))
            .ToList();

        var attackFrames = atlas.GetRegionNames()
            .Where(name => name.StartsWith("attack"))
            .OrderBy(name => int.Parse(name.Split('_')[1]))
            .Select(name => atlas.GetRegion(name))
            .ToList();

        if (idleFrames.Count == 0)
            throw new Exception("No idle frames found");

        if (attackFrames.Count == 0)
            throw new Exception("No attack frames found");

        var idle = new Animation(idleFrames, FrameTime);
        var attack = new Animation(attackFrames, FrameTime);

        
        float speed = GetSpeed(PlantType.Chomper); 

        idle.SetSpeed(speed);
        attack.SetSpeed(speed);

        return (idle, attack);
    }
    private float GetSpeed(PlantType type)
    {
        return AnimationSpeeds.TryGetValue(type, out var s) ? s : 1f;
    }
}

    