public interface IGameState
{
    int Sun { get; }
    int CurrentWave { get; }
    GameStatus Status { get; }

    void AddSun(int amount);
    bool SpendSun(int amount);
    void AdvanceWave();
    void SetStatus(GameStatus status);
}
