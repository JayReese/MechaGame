using UnityEngine;
using System;
using System.Collections;

public enum PlayerState { ON_GROUND, BOOSTING };

public enum LockOnState { FREE, LOCKED };

public enum LockOnHardness { SOFT, MEDIUM, HARD };

public delegate void CommandExecution();
public delegate Transform ActiveLockOnTarget();

public static class Globals
{
    
}
