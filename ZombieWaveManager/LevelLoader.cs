using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

/*
* This class is a function class that read the .XML file that contain wave info.
* It returns an object of LevelSpawnData class to pass the information to ZombieWaveManager,
*/
public static class LevelLoader
{
    public static LevelSpawnData LoadFromXml(string filePath)
    {
        XDocument doc = XDocument.Load(filePath);
        XElement levelElement = doc.Element("Level")
            ?? throw new Exception("Missing Level root element.");

        LevelSpawnData levelData = new LevelSpawnData
        {
            LevelName = (string?)levelElement.Attribute("Name") ?? "UnnamedLevel",
            LevelDuration = (float?)levelElement.Attribute("Duration") ?? 0f
        };

        foreach (XElement waveElement in levelElement.Elements("Wave"))
        {
            WaveData wave = new WaveData
            {
                WaveIndex = (int?)waveElement.Attribute("Index") ?? 0,
                StartTime = (float?)waveElement.Attribute("StartTime") ?? 0f,
                IsHugeWave = (bool?)waveElement.Attribute("IsHugeWave") ?? false
            };

            foreach (XElement spawnElement in waveElement.Elements("SpawnEvent"))
            {
                string laneText = (string?)spawnElement.Attribute("Lanes") ?? "";
                List<int> lanes = laneText
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.Parse(s.Trim()))
                    .ToList();

                SpawnEvent spawnEvent = new SpawnEvent
                {
                    TriggerTime = (float?)spawnElement.Attribute("Time") ?? 0f,
                    ZombieType = Enum.Parse<ZombieType>((string?)spawnElement.Attribute("ZombieType") ?? "Normal"),
                    Count = (int?)spawnElement.Attribute("Count") ?? 1,
                    AllowedLanes = lanes,
                    WaveIndex = wave.WaveIndex
                };

                wave.SpawnEvents.Add(spawnEvent);
            }

            levelData.Waves.Add(wave);
        }

        return levelData;
    }
}