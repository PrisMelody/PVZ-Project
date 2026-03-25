using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

/*
* This class manage all the zombies that are currently on the level and alive.
* It adds new zombie as ZombieSpawnManager requests.
* It updates zombies and remove dead zombies.
*/
public class ZombieManager : IPvZDrawable, IPvZUpdatable
{
    private readonly ICollectableSpawner _collectableSpawner;

    private readonly List<IZombie>[] _zombies = 
    {new List<IZombie>(), new List<IZombie>(), new List<IZombie>(), new List<IZombie>(), new List<IZombie>()};
    public int DrawOrder { get; set; }
    public IReadOnlyList<List<IZombie>> ZombiesByLane => _zombies;

    public ZombieManager(ICollectableSpawner collectableSpawner = null)
    {
        _collectableSpawner = collectableSpawner;
    }

    public void Add(IZombie zombie)
    {
        _zombies[zombie.Lane].Add(zombie);
    }

    public void Update(GameTime gameTime)
    {
        for (int i = 0; i <= 4; i++) //TODO: remove magic numbers
        {
            for (int j = _zombies[i].Count - 1; j >= 0; j--)
            {
                _zombies[i][j].Update(gameTime);
                if (_zombies[i][j].IsDead){
                    _collectableSpawner?.SpawnCoinAt(new Vector2(_zombies[i].xCoord, _zombies[i].yCoord));
                    _zombies[i].RemoveAt(j);
                }  
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i <= 4; i++) //TODO: remove magic numbers
        {
            foreach (var zombie in _zombies[i])
            zombie.Draw(spriteBatch);
        }
        
    }
    public bool HasAnyAliveZombies()
    {
        return _zombies.Any(z => !z.IsDead);
    }

    public bool HasAliveZombiesInWave(int waveIndex)
    {
        return _zombies.Any(z => !z.IsDead && z.SpawnWaveIndex == waveIndex);
    }

}
