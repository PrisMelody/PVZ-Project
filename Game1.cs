using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MonoGameLibrary.Sprites;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    private TextureAtlas testAtlas;
    private Sprite testSprite;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        // --- Create a dummy texture ---
        Texture2D dummyTexture = new Texture2D(GraphicsDevice, 64, 64);
        Color[] pixels = new Color[64 * 64];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = Color.Green; // fill with green
        dummyTexture.SetData(pixels);

        // --- Create atlas and regions ---
        testAtlas = new TextureAtlas(dummyTexture);
        testAtlas.AddRegion(new TextureRegion("GreenSquare", new Rectangle(0, 0, 32, 32)));

        // --- Create a test sprite from atlas ---
        testSprite = new TestSprite(testAtlas);
        testSprite.Position = new Vector2(100, 100);
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();
        testSprite.Draw(spriteBatch);
        spriteBatch.End();

        base.Draw(gameTime);
    }
}

// --- Example derived sprite ---
public class TestSprite : Sprite
{
    public TestSprite(TextureAtlas atlas) : base(atlas, "GreenSquare") { }
}