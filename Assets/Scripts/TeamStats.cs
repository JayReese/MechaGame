using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct TeamStats
{
    public int TeamNumber { get; private set; }
    public int Score { get; private set; }
    public Color TeamColor;
    public List<GameObject> TeamMembers;
    public Transform[] SpawnPositions;

    public TeamStats(int tn) : this()
    {
        Score = 0;
        TeamNumber = tn;
        TeamColor = TeamNumber == 1 ? Color.red : Color.white;
        TeamMembers = new List<GameObject>();

        Debug.Log(TeamNumber + ": " + TeamColor);
    }

    public void SetUpTeamSpawns(int index, Transform spawn)
    {
        SpawnPositions[index] = spawn;
    }

    public void AddPlayerToTeam(GameObject playerOnTeam)
    {
        TeamMembers.Add(playerOnTeam);
    }

    public void AddToScore(int amountToAdd)
    {
        Score += amountToAdd;
    }
}
