using System.Collections.Generic;

/*
* This class is to store info of a single wave inside a level.
* The list SpawnEvents stores a list of event that trigger zombie spawning inside current wave.
* One single SpawnEventData can only spawn a single type of zombies (though the count is unlimited).
* If you want to spawn multiple types at once, should use multiple spawnevnets.
*/
public class WaveData
{
    public int WaveIndex { get; set; }
    public float StartTime { get; set; }
    public bool IsHugeWave { get; set; }

    public List<SpawnEvent> SpawnEvents { get; set; } = new();
}