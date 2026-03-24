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
    private readonly List<IZombie> _zombies = new();
    private readonly ICollectableSpawner _collectableSpawner;

    public int DrawOrder { get; set; }
    public IReadOnlyList<IZombie> Zombies => _zombies;

    public ZombieManager(ICollectableSpawner collectableSpawner = null)
    {
        _collectableSpawner = collectableSpawner;
    }

    public void Add(IZombie zombie)
    {
        _zombies.Add(zombie);
    }

    public void Update(GameTime gameTime)
    {
        for (int i = _zombies.Count - 1; i >= 0; i--)
        {
            _zombies[i].Update(gameTime);
            if (_zombies[i].IsDead)
            {
                _collectableSpawner?.SpawnCoinAt(new Vector2(_zombies[i].xCoord, _zombies[i].yCoord));
                _zombies.RemoveAt(i);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var zombie in _zombies)
            zombie.Draw(spriteBatch);
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
