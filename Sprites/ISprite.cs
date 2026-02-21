public interface ISprite
{
    TextureRegion Region { get; set; }
    
    Vector2 Origin { get; set; }
    Color Color { get; set; }
    float Rotation { get; set; }
    Vector2 Scale { get; set; }
    float LayerDepth { get; set; }

    void Draw(SpriteBatch spriteBatch, Vector2 position);
}