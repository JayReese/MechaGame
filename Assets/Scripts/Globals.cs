using UnityEngine;
using System;
using System.Collections;
using System.IO;

public enum BoostState { ON_GROUND, BOOSTING };

public enum PoiseState { POISED, DAZED = 50, SUPER_STAGGERED = 100 };

public enum InterfacingState { CONTROLLABLE = 1, SPECTATING, NONE };

public enum RoundState { NOT_STARTED, IN_PROGRESS, ENDED }

public enum LockOnState { FREE, LOCKED };

public enum LockOnHardness { SOFT = 1, HARD };

public enum ArmorPiercingInteraction { BLOCKED = 1, DAMAGING, PIERCING, BREAKING };

public enum SurfaceType { PLAYER, ARMOR, ENVIRONMENT };

public enum GameState { MAIN_MENU = 1, CHARACTER_SELECT, MAIN_GAME, END_OF_MATCH };

public delegate void CommandExecution();
public delegate void MovementBehavior();

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

    /// <summary>
    /// Extension method to find a grandchild.
    /// </summary>
    /// <param name="aParent"></param>
    /// <param name="aName"></param>
    /// <returns></returns>
    public static Transform FindGrandchild(this Transform aParent, string aName)
    {
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = child.FindGrandchild(aName);
            if (result != null)
                return result;
        }
        return null;
    }

    /// <summary>
    /// Extension method that iterates up the directory tree to find the grandparent you want.
    /// </summary>
    /// <param name="aCurrent"></param>
    /// <param name="aNameToStop"></param>
    /// <returns></returns>
    public static Transform FindGrandparent(this Transform aCurrent, string aNameToStop)
    {
        Transform currentParent = aCurrent;

        while (currentParent.parent.name != aNameToStop)
            currentParent = aCurrent.parent;

        return currentParent;
    }

    /// <summary>
    /// Returns the number of active children.
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static int ActiveChildrenCount(this Transform t)
    {
        int k = 0;
        foreach (Transform c in t)
        {
            if (c.gameObject.activeSelf)
                k++;
        }
        return k;
    }

    /// <summary>
    /// Play a layered sound clip at your volume.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="audioIndexToSearch"></param>
    /// <param name="audioClipRetrievedFromIndex"></param>
    /// <param name="volumeOfClip"></param>
    public static void PlaySoundClip(AudioSource source, int audioIndexToSearch, int audioClipRetrievedFromIndex, float volumeOfClip = 0.7f)
    {
        source.PlayOneShot(SoundManager.GetSoundClipForAllocation(audioIndexToSearch - 1, audioClipRetrievedFromIndex - 1), volumeOfClip);
    }

    public static string ReturnChosenDirectory(string path)
    {
        return string.Format("{0}/{1}", Directory.GetCurrentDirectory() + "\\Assets\\", path);
    }
}
