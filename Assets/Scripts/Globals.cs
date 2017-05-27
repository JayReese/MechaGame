using UnityEngine;
using System;
using System.Collections;

public enum PlayerState { ON_GROUND, BOOSTING };

public enum LockOnState { FREE, LOCKED };

public enum LockOnHardness { SOFT = 1, HARD };

public enum ArmorPiercingInteraction { BLOCKED = 1, DAMAGING, PIERCING, BREAKING };

public delegate void CommandExecution();
public delegate void MovementBehavior();

public delegate Transform ActiveLockOnTarget(); // Not really necessary anymore.

public static class Globals
{
    public static RaycastHit RaycastHitTarget(Vector3 origin, Vector3 direction, float range)
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(origin, direction, out hit, range))
            return hit;

        return hit;
    }
}
