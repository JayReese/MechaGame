using UnityEngine;
using System;
using System.Collections;

public enum PlayerState { ON_GROUND, BOOSTING };

public enum LockOnState { FREE, LOCKED };

public enum LockOnHardness { SOFT = 1, HARD };

public enum ArmorPiercingInteraction { BLOCKED = 1, DAMAGING, PIERCING, BREAKING };

public enum SurfaceType { PLAYER, ARMOR, ENVIRONMENT };

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

    public static Vector2 ReturnCorrectCameraRect(int idToReturn)
    {
        Vector2[] cameraRects = new Vector2[] { new Vector2(0, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0, 0), new Vector2(0.5f, 0) };

        return cameraRects[idToReturn];
    }
}
