using UnityEngine;
using System;
using System.Collections;

public enum PlayerState { ON_GROUND, BOOSTING };

public enum LockOnState { FREE, LOCKED };

public enum LockOnHardness { SOFT, MEDIUM, HARD };

public enum ArmorPiercingInteraction { BLOCKED = 1, DAMAGING, PIERCING };

public delegate void CommandExecution();
public delegate void MovementBehavior();
public delegate Transform ActiveLockOnTarget();

public static class Globals
{
    
}
