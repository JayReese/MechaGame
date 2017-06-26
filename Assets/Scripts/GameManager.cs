using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    /*
     * READ ME: I have no idea how or what information is going to be passed to me from the character select screen
     * I want to assume that players pick which team they're on, and they already know 
     * 
     * Assuming I have almost NO info from the previous scene, the current purpose of this class is to:
     * 1) Instantiate and distribute players evenly across teams
     * 2) Keep track of each team's score
     * 3) Change scenes if a certain team wins
     */

    [SerializeField] int pointsToWinThreshhold;
    [SerializeField] float MatchStartTimer;
    [SerializeField] GameObject[] mechaPlayerPrefabs;
    [SerializeField] TeamTransformPositions[] TeamSpawnPositions;
    //[SerializeField]
    //GameObject[] TeamSpawningPositions;
    [SerializeField]
    List<Transform> BuildingWaypoints, RegularWaypoints;
    //[SerializeField]
    //List<GameObject> TeamOne, TeamTwo;
    [SerializeField]
    TeamStats[] Teams;

    public const int pointsPerKill = 100;       //how many points the team will score when they get a kill
    const int numberOfTeams = 2;                //this variable can be adjusted in the future if we ever want more than 2 teams
    int NumPlayers;
    int[] teamScoreboards;

    GameObject PlayersInMatch;

    //You can't have 2D arrays exposed to the editor, so this is the workaround
    [System.Serializable]
    struct TeamTransformPositions
    {
        public Transform[] positions;
    }


    void Awake()
    {

        //CreatePlayers();
        //Players = GameObject.FindGameObjectsWithTag("Controllable");

        //determine how many controllers are hooked up
        NumPlayers = Input.GetJoystickNames().Length;
        //Debug.Log(players);
        teamScoreboards = new int[NumPlayers];
        //bool unevenTeams = (players % numberOfTeams != 0);    //this variable can be used to determine if the teams cannot be evenly distributed
        PlayersInMatch = GameObject.Find("Players");

        SetUpPlayersForBattle();
        SetUpTeams();
        
    }

    private void SetUpTeams()
    {
        Teams = new TeamStats[2];

        // All players in the match are iterated through and added to the team member list to ensure that they are with their correct teammate.
        #region Assigns players to the correct teams.
        for (byte i = 0; i < Teams.Length; i++)
        {
            Teams[i] = new TeamStats(i + 1);

            for(byte a = 0; a < PlayersInMatch.transform.childCount; a++)
            {
                if (PlayersInMatch.transform.GetChild(a).GetComponent<Player>().TeamNumber == Teams[i].TeamNumber)
                    Teams[i].AddPlayerToTeam(PlayersInMatch.transform.GetChild(a).gameObject);
            }
        }
        #endregion

        SetUpTeamSpawnPositions();

        RepositionAllPlayersToRespectiveSpawns();

        SetPlayerColors();
    }

    // Use this for initialization
    void Start ()
    {
        GetWaypointTransformReferences();
        MatchStartTimer = 5;
    }

    void Update()
    {
#if UNITY_EDITOR
        PerformScoreDebugging();
#endif

        for(byte i = 0; i < Teams.Length; i++)
        {
            if (Teams[i].Score >= 4)
                Debug.Log("Team " + Teams[i].TeamNumber + " wins.");
        }

        ToggleRoundProgress();
    }

    private void ToggleRoundProgress()
    {
        if (MatchStartTimer > 0)
            MatchStartTimer -= Time.deltaTime;
        else
            StartMatch();
    }

    #region Match start methods and routines to perform.
    void GetWaypointTransformReferences()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Waypoint");

        BuildingWaypoints = new List<Transform>();
        RegularWaypoints = new List<Transform>();

        foreach (GameObject g in go)
        {
            if (g.name == "Building Waypoint")
                BuildingWaypoints.Add(g.transform);
            else
                RegularWaypoints.Add(g.transform);
        }
    }

    void SetPlayerID()
    {
        for (int i = 0; i < PlayersInMatch.transform.childCount; i++)
        {
            PlayersInMatch.transform.GetChild(i).GetComponent<Player>().PlayerID = i;        

            Vector2 viewRect = Globals.ReturnCorrectCameraRect(i);
            PlayersInMatch.transform.GetChild(i).GetComponentInChildren<Camera>().rect = new Rect(viewRect[0], viewRect[1], 0.5f, 0.5f);
        }
    }

    void SetUpTeamSpawnPositions()
    {
        for (byte i = 0; i < Teams.Length; i++)
        {
            if (Teams[i].SpawnPositions == null) Teams[i].SpawnPositions = new Transform[PlayersInMatch.transform.childCount / 2];

            for (byte a = 0; a < Teams[i].SpawnPositions.Length; a++)
                Teams[i].SetUpTeamSpawns(a, GameObject.Find("Team Spawns").transform.GetChild(Teams[i].TeamNumber - 1).GetChild(a));
        }
    }

    void AssignTeamNumberToPlayers()
    {

        for (byte i = 0; i < PlayersInMatch.transform.childCount; i++)
        {
            if (PlayersInMatch.transform.GetChild(i).GetComponent<Player>().PlayerID == 0 || PlayersInMatch.transform.GetChild(i).GetComponent<Player>().PlayerID == 2)
                PlayersInMatch.transform.GetChild(i).GetComponent<Player>().SetTeamNumber(1);
            else
                PlayersInMatch.transform.GetChild(i).GetComponent<Player>().SetTeamNumber(2);
        }
    }

    void CreatePlayers()
    {
        for (byte i = 0; i < 4; i++)
            Instantiate(Resources.Load("Prefabs/Testing/Test Machi") as GameObject);
    }

    private void StartMatch()
    {
        Debug.Log("Match begin.");

        MakeAllPlayersControllable();
    }

    private void MakeAllPlayersControllable()
    {
        for (byte i = 0; i < PlayersInMatch.transform.childCount; i++)
            PlayersInMatch.transform.GetChild(i).GetComponent<Player>().CurrentInterfacingState = InterfacingState.CONTROLLABLE;
    }

    //void ActivateWin()
    //{
    //    //do more stuff (like register scores, calculate MVP, send info to the next scene, signal the UI to update/display end of match stats, etc)

    //    StateManager.instance.currentGameState = StateManager.GameState.END_OF_MATCH;
    //}

    void SetUpPlayersForBattle()
    {
        SetPlayerID();
        AssignTeamNumberToPlayers();
    }

    public void ActivateSpawn(GameObject player)
    {
        int teamIndexToCheck = player.GetComponent<Player>().TeamNumber - 1;

        for(byte i = 0; i < Teams[teamIndexToCheck].TeamMembers.Count; i++)
        {
            if (player == Teams[teamIndexToCheck].TeamMembers[i])
                player.transform.position = Teams[teamIndexToCheck].SpawnPositions[i].position;
        }
    }

    private void RepositionAllPlayersToRespectiveSpawns()
    {
        for(byte i = 0; i < Teams.Length; i++)
        {
            foreach (GameObject p in Teams[i].TeamMembers)
                ActivateSpawn(p);
        }
    }

    private void SetPlayerColors()
    {
        for(byte i = 0; i < Teams.Length; i++)
        {
            foreach (GameObject p in Teams[i].TeamMembers)
                if(p.name.Contains("Test")) p.GetComponent<Player>().ChangeBodyColor(Teams[i].TeamColor);
        }
    }
    #endregion

#if UNITY_EDITOR
    private void PerformScoreDebugging()
    {
        int teamThatScored = -1;
        int teamAttacked = -1;

        if(Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.P))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                teamThatScored = 1;
                teamAttacked = 2;
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                teamThatScored = 2;
                teamAttacked = 1;
            }

            Teams[teamThatScored - 1].AddToScore(1);
        }
        

        
    }
#endif    
}
