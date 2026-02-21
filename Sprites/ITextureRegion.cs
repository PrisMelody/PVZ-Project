 public interface ITextureRegion
    {
       
        Texture2D Texture { get; set; }

       
        Rectangle SourceRectangle { get; set; }

        //
        int Width { get; }

       
        int Height { get; }
    
    }