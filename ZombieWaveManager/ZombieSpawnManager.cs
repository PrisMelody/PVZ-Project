using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

/*
* This class is a dispatch center for spawning zombies.
* It holds a ZombieManager instance to manage active zombies
* and use ZombieFactory instance to spawn new zombie and add it to ZombieManager.
*/
public class ZombieSpawnManager
{
    private readonly LevelSpawnData levelData;
    private readonly ZombieManager zombieManager;
    private readonly ZombieFactory zombieFactory;
    private readonly Random random = new();

    private float elapsedTime;
    private int currentWaveIndex;
    private bool levelCompleted;

    public bool LevelCompleted => levelCompleted;

    // Events.
    public event Action<int>? OnWaveStarted;
    public event Action<int>? OnHugeWaveStarted;
    public event Action? OnLevelCompleted;

    public ZombieSpawnManager(LevelSpawnData levelData, ZombieManager zombieManager, ZombieFactory zombieFactory)
    {
        this.levelData = levelData;
        this.zombieManager = zombieManager;
        this.zombieFactory = zombieFactory;
        this.elapsedTime = 0f;
        this.currentWaveIndex = 0;
    }

    public void Update(GameTime gameTime)
    {
        if (levelCompleted) return;

        elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (currentWaveIndex >= levelData.Waves.Count)
        {
            CheckLevelCompletion();
            return;
        }

        WaveData currentWave = levelData.Waves[currentWaveIndex];

        // If current time has passed event time, then begin spawning.
        if (elapsedTime >= currentWave.StartTime)
        {
            ProcessWave(currentWave);
        }

        // If all zombie of current wave is eliminated, then switch to next wave.
        if (IsWaveFinished(currentWave))
        {
            currentWaveIndex++;

            if (currentWaveIndex >= levelData.Waves.Count)
            {
                CheckLevelCompletion();
            }
            else
            {
                WaveData nextWave = levelData.Waves[currentWaveIndex];
                if (nextWave.IsHugeWave)
                {
                    OnHugeWaveStarted?.Invoke(nextWave.WaveIndex);
                }
                else
                {
                    OnWaveStarted?.Invoke(nextWave.WaveIndex);
                }
            }
        }
    }

    private void ProcessWave(WaveData wave)
    {
        foreach (var spawnEvent in wave.SpawnEvents)
        {
            if (!spawnEvent.Triggered && elapsedTime >= spawnEvent.TriggerTime)
            {
                SpawnZombies(spawnEvent);
                spawnEvent.Triggered = true;
            }
        }
    }

    private void SpawnZombies(SpawnEvent spawnEvent)
    {
        List<int> lanePool = spawnEvent.AllowedLanes == null || spawnEvent.AllowedLanes.Count == 0
            ? new List<int> { 0, 1, 2, 3, 4 }
            : new List<int>(spawnEvent.AllowedLanes);
        List<int> availableLanes = new List<int>(lanePool);

        for (int i = 0; i < spawnEvent.Count; i++)
        {
            if (availableLanes.Count == 0)
            {
                availableLanes = new List<int>(lanePool);
            }

            int lane = PickLane(availableLanes);
            IZombie zombie = zombieFactory.CreateZombie(spawnEvent.type, lane);

            // Mark the zombie wave index with spawn wave index.
            zombie.SpawnWaveIndex = spawnEvent.WaveIndex;

            zombieManager.Add(zombie);
            availableLanes.Remove(lane);
        }
    }

    private int PickLane(List<int> allowedLanes)
    {
        if (allowedLanes == null || allowedLanes.Count == 0)
            return random.Next(0, 5); 

        int index = random.Next(allowedLanes.Count);
        return allowedLanes[index];
    }

    private bool IsWaveFinished(WaveData wave)
    {
        bool allEventsTriggered = wave.SpawnEvents.All(e => e.Triggered);
        bool anyAliveInWave = zombieManager.HasAliveZombiesInWave(wave.WaveIndex);

        return allEventsTriggered && !anyAliveInWave;
    }

    private void CheckLevelCompletion()
    {
        if (!zombieManager.HasAnyAliveZombies())
        {
            levelCompleted = true;
            OnLevelCompleted?.Invoke();
        }
    }
}
