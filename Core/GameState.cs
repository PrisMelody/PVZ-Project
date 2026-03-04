public class GameState : IGameState
{
    public int Sun { get; private set; }
    public int CurrentWave { get; private set; }
    public GameStatus Status { get; private set; }

    public GameState(int startingSun = 50)
    {
        Sun = startingSun;
        CurrentWave = 0;
        Status = GameStatus.Playing;
    }

    public void AddSun(int amount)
    {
        if (amount > 0)
            Sun += amount;
    }

    public bool SpendSun(int amount)
    {
        if (amount <= 0 || Sun < amount)
            return false;

        Sun -= amount;
        return true;
    }

    public void AdvanceWave()
    {
        CurrentWave++;
    }

    public void SetStatus(GameStatus status)
    {
        Status = status;
    }
}
