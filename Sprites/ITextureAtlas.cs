

public interface ITextureAtlas
{   
   
    Texture2D Texture { get; set; }
    
    void AddRegion(string name, int x, int y, int width, int height);
   
    TextureRegion GetRegion(string name);
    
    bool RemoveRegion(string name);
             
    void Clear();
    Sprite CreateSprite(string regionName);

    




    

}
