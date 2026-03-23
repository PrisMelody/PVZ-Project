# PVZ-Project

Basic demo! Click on a seed packet in order to select the plant that will appear.
Zombies will walk in from the left side of the screen, and there's nothing you can do about it! (Luckily, we haven't implemented death)

This version does not have all of the plants we plan to include, and the zombie sprites aren't properly animated.
Additionally, it doesn't include some of our controller classes, since we didn't have time to get those working properly.

Collisions notes: (REMOVE BEFORE MERGE!)

- Divide zombies into spawn by lanes
- Projectiles only check zombies in the same lane.
    - The projectiles don't need to neccessarily store thier own lane, it could be handled by the entityManager.
- Zombies only check plants in their lane.
- Explosions check for zombies in every lane.


Current Structure:

For each lane:
    For each projectile:
        For each Zombie:
            Check if the projectile is colliding with the zombie in this lane. (Zombies that spawned earlier will be first in the queue and will take priority.)
            If true, stop and set the projectile to isExpired (or equivilent).
    For each zombie
        set is attacking to false
        for each plant:
            check if it's in front of a plant. If it is, damage the plant (some kind of limiter?), set isAttacking to true, and exit.
    For each explosion
        For each zombie
            Check if the zombie is in the explosion (within some radius/rectangle)
        Set the explosion to isExpired (or equivilent).

    Remove all dead zombies/plants and expired projectiles.