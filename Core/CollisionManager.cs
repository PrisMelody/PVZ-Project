using System.Collections.Generic;

public class CollisionManager
{
    private ZombieManager _zombieManager;
    private Map _map;

    readonly public static Dictionary<float, int> getLaneFromYPos = new Dictionary<float, int>(){
            { 120.0f, 0 },
            { 210.0f, 1 }, 
            { 300.0f, 2 },
            { 390.0f, 3 },
            { 480.0f, 4 },
        };

    public CollisionManager(ZombieManager zombieManager, Map map)
    {
        _zombieManager = zombieManager;
        _map = map;
    }

    public void CheckZombiePlantCollision(int lane) //TODO: stick this in its own class. 
    {
        foreach(IZombie zombie in _zombieManager.ZombiesByLane[lane])
        {
            //bool hasTarget = false;
            foreach (IGridPlot currentGrid in _map._grid.Lanes[lane].Plots) //This is gross.
            {
                if (!currentGrid.IsOccupied){continue;}
                float distance = zombie.xCoord - currentGrid.Plant.XPos;
                if (distance < zombie.MaxRange && distance > zombie.MinRange)
                {
                    zombie.Attack(currentGrid.Plant);
                }
                break;
            }
        } 
    }

    public void CheckProjectileZombieCollision()
    {
        foreach (IProjectile projectile in _map.Projectiles)
        {
            int lane = getLaneFromYPos[projectile.YPos];
            foreach(IZombie zombie in _zombieManager.ZombiesByLane[lane])
            {
                float distance = zombie.xCoord - projectile.XPos;
                if(distance < 0  && distance > -30)
                    projectile.OnHit(zombie);
                if (projectile.IsDead)
                    break;
            }
        }
    }

    public void CheckSplashZombieCollision()
    {
        //TODO: once cherry bombs and mines are set up, add this.
        //This will probably end up being n^2, but given that explosions last for a single frame that's probably fine.
    }
}