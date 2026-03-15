using System.Collections.Generic;
using PlantsVsZombies.entities;
using PlantsVsZombies.Zombies;

namespace PlantsVsZombies.Controllers
{
    public class CollisionManager
    {
        public void CheckProjectileZombieCollision(
            List<PlantsVsZombies.entities.Projectile> projectiles,
            List<IZombie> zombies)
        {
            foreach (var projectile in projectiles)
            {
                foreach (var zombie in zombies)
                {
                    if (projectile.Hitbox.Intersects(zombie.Hitbox))
                    {
                        zombie.TakeDamage(projectile.Damage);
                        break;
                    }
                }
            }
        }
    }
}