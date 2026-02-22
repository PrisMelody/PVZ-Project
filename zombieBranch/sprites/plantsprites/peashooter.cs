using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary.Sprites;
using System.Collections.Generic;
using System.Linq;

namespace PVZProject.sprites.plantsprites;

public class Peashooter : IUpdatable, IDrawable
{
    private AnimatedSprite _sprite;
    public int DrawOrder => 1;

    public Peashooter(ContentManager content, Vector2 position)
    {
        // Load atlas, frames, animation, sprite
        // Load XML atlas
        ITextureAtlasLoader loader = new XmlTextureAtlasLoader();
        TextureAtlas atlas = loader.Load(content, "plants"); // plants.xml in Content folder

        // Get all frames starting with "peashooter_"
        List<ITextureRegion> frames = atlas.GetRegionsStartingWith("peashooter_");

        // Create animation
        Animation anim = new Animation(frames, 0.15f);

        // Create AnimatedSprite
        _sprite = new AnimatedSprite(anim, position);
    }

    public void Update(GameTime gameTime)
    {
        _sprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch);
    }
}