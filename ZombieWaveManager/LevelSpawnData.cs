using System.Collections.Generic;

/*
* This class stores the information of the current level into the system.
* LevelLoader would load a .XML file and return a LevelSpawnData class.
* The list Waves stores a list of all waves inside current level.
*/
public class LevelSpawnData
{
    public string LevelName { get; set; } = "";
    public float LevelDuration { get; set; }
    public List<WaveData> Waves { get; set; } = new();
}