using System.Collections.Generic;


/*
* This class is an event to spawn a single type of zombies.
*/
public class SpawnEvent
{
    // The time this event is triggered.
    public float TriggerTime { get; set; }
    public int WaveIndex { get; set; }
    public ZombieType type { get; set; }
    public int Count { get; set; }
    public bool Triggered { get; set; }
    // Which lanes are allowed to spawn zombie.
    public List<int> AllowedLanes { get; set; } = new();
}