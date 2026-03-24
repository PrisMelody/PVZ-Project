using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IPlantState
{
    void Enter();                     // Called when the state starts
    void Update(GameTime gameTime);   // Called every frame
    void Exit();                      // Called when leaving the state
}
