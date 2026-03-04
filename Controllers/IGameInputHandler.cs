using Microsoft.Xna.Framework;

/// <summary>
/// Implemented by the game to handle input events. Controllers call this instead of
/// depending on Game1 directly, so input logic stays behind an interface.
/// </summary>
public interface IGameInputHandler
{
    /// <summary>
    /// Called when the user clicks. The game decides whether the click is on the
    /// plant menu, grid, shovel, etc., and runs the appropriate command.
    /// </summary>
    void HandleClick(Point screenPosition);
}
