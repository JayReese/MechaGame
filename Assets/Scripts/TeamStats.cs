using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct TeamStats
{
    public int TeamNumber { get; private set; }
    public int Score { get; private set; }
    public int ActiveMembers { get; private set; }
    public Color TeamColor;
    public List<GameObject> TeamMembers, ActiveTeamMembers;
    public Transform[] SpawnPositions;

    public TeamStats(int tn) : this()
    {
        Score = 0;
        TeamNumber = tn;
        TeamColor = TeamNumber == 1 ? Color.red : Color.white;
        TeamMembers = new List<GameObject>();
        ActiveTeamMembers = new List<GameObject>();

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

    public void PopulateActivePlayList()
    {
        foreach (GameObject g in TeamMembers)
            ActiveTeamMembers.Add(g);
    }

    public void RemoveFromActivePlay(GameObject player)
    {
        if(ActiveTeamMembers.Count != 0)
        {
            for(byte i = 0; i < ActiveTeamMembers.Count; i++)
            {
                if (player == ActiveTeamMembers[i])
                {
                    ActiveTeamMembers.RemoveAt(i);
                    break;
                }
            }
        }
    }

    public bool AllPlayersDowned()
    {
        bool allDowned = true;

        foreach(GameObject g in TeamMembers)
        {
            if (g.GetComponent<DamageableObject>().Health > 0)
                allDowned = false;
        }

        return allDowned;
    }
}
