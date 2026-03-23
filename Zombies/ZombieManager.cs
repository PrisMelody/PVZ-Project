using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ZombieManager : IPvZDrawable, IPvZUpdatable
{
    private readonly List<IZombie>[] _zombies = 
    {new List<IZombie>(), new List<IZombie>(), new List<IZombie>(), new List<IZombie>(), new List<IZombie>()};
    public int DrawOrder { get; set; }
    public IReadOnlyList<List<IZombie>> ZombiesByLane => _zombies;

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
                if (_zombies[i][j].IsDead)
                    _zombies[i].RemoveAt(j);
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
}
