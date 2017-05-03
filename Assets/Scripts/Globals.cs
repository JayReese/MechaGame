using UnityEngine;
using System;
using System.Collections;

public enum PlayerState { ON_GROUND, BOOSTING };

public enum LockOnState { FREE, LOCKED };

public delegate void CommandExecution();

public static class Globals
{
    public static void CommandExecutables(Action a)
    {
        a();
    }
}
