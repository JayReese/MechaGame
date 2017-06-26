using UnityEngine;
using System.Collections;

public struct PlayerMatchStats
{
    GameObject OwnerOfStats;
    float CurrentRespawnTime;

    public PlayerMatchStats(GameObject tetheredPlayer) : this()
    {
        OwnerOfStats = tetheredPlayer;
        CurrentRespawnTime = 0;
    }

    void CheckForPlayerDeath()
    {

    }
}
