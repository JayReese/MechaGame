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

    [SerializeField] int CurrentMatchProgress;
    [SerializeField] const int MaxPointsToWin = 5;
    [SerializeField] float RoundStartTime;
    [SerializeField] RoundState CurrentRoundState;
    [SerializeField] GameObject[] mechaPlayerPrefabs;
    //[SerializeField]
    //GameObject[] TeamSpawningPositions;
    [SerializeField]
    List<Transform> BuildingWaypoints, RegularWaypoints, AllPlayersInMatch;
    //[SerializeField]
    //List<GameObject> TeamOne, TeamTwo;
    [SerializeField]
    TeamStats[] Teams;


    public const int pointsPerKill = 100;       //how many points the team will score when they get a kill
    const int numberOfTeams = 2;                //this variable can be adjusted in the future if we ever want more than 2 teams
    int NumPlayers;
   
    void Awake()
    {
        RoundStartTime = 5;
        
        #region commented out - player tracking and instantiation.
        //bool unevenTeams = (players % numberOfTeams != 0);    //this variable can be used to determine if the teams cannot be evenly distributed
        ////determine how many controllers are hooked up
        //NumPlayers = Input.GetJoystickNames().Length;
        //CreatePlayers();
        #endregion

        PopulateGlobalListOfPlayers();
        SetUpPlayersForBattle();
        SetUpTeams();

        if (CurrentMatchProgress == 0) CurrentRoundState = RoundState.NOT_STARTED;
    }

    private void PopulateGlobalListOfPlayers()
    {
        //Debug.Log(GameObject.Find("Players").transform.GetChild(3).name);

        for (byte a = 0; a < GameObject.Find("Players").transform.childCount; a++)
        {
            //Debug.Log(GameObject.Find("Players").transform.GetChild(a).name);
            AllPlayersInMatch.Add(GameObject.Find("Players").transform.GetChild(a));
        }
    }

    private void SetUpTeams()
    {
        Teams = new TeamStats[2];

        // All players in the match are iterated through and added to the team member list to ensure that they are with their correct teammate.
        #region Assigns players to the correct teams.
        for (byte i = 0; i < Teams.Length; i++)
        {
            Teams[i] = new TeamStats(i + 1);

            for(byte a = 0; a < AllPlayersInMatch.Count; a++)
            {
                if (AllPlayersInMatch[a].GetComponent<Player>().TeamNumber == Teams[i].TeamNumber)
                    Teams[i].AddPlayerToTeam(AllPlayersInMatch[a].gameObject);    
            }
        }
        #endregion

        SetUpTeamSpawnPositions();

        //SetPlayerColors();
    }

    // Use this for initialization
    void Start ()
    {
        GetWaypointTransformReferences();
        PerformStartOfRoundDuties();
        //BeginGame();
    }

    private void PerformStartOfRoundDuties()
    {
        // Sets the starting of the new round to true if we haven't reached the maximum amount.
        if (CurrentRoundState == RoundState.NOT_STARTED) CurrentMatchProgress = 1;

        if (CurrentRoundState == RoundState.ENDED)
        {
            //CurrentMatchProgress++;
            CurrentRoundState = RoundState.IN_PROGRESS;
        }

        BeginGame();
    }

    void Update()
    {
#if UNITY_EDITOR
        PerformScoreDebugging();
#endif
        
        //CheckIfAnyLiveEntitiesAreDead();
        //PerformDutiesWhileNotInCombat();
        PerformMidRoundDuties();
    }

    private void PerformMidRoundDuties()
    {
        CheckIfAnyLiveEntitiesAreDead();
        PerformEndOfRoundDuties();
    }

    private void PerformEndOfRoundDuties()
    {
        if(CurrentRoundState == RoundState.ENDED && MatchCurrentlyInProgress())
        {
            Debug.Log("The round has ended.");

            Debug.Log(string.Format("Tean 1: {0}\nTeam 2: {1}", Teams[0].Score, Teams[1].Score));

            PerformStartOfRoundDuties();
        }

        if (!MatchCurrentlyInProgress())
            ShowResults();
    }

    private void ShowResults()
    {
        for (byte i = 0; i < Teams.Length; i++)
        {
            if (Teams[i].Score == 5)
                Debug.Log("Team " + (i + 1) + " wins!");
        }
    }


    #region Match start methods and routines to perform.
    void GetWaypointTransformReferences()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Waypoint");

        BuildingWaypoints = new List<Transform>();
        RegularWaypoints = new List<Transform>();

        foreach (GameObject g in go)
        {
            if (g.name.Contains( "Building" ))
                BuildingWaypoints.Add(g.transform);
            else
                RegularWaypoints.Add(g.transform);
        }
    }

    void SetPlayerID()
    {
        for (int i = 0; i < AllPlayersInMatch.Count; i++)
        {
            AllPlayersInMatch[i].GetComponent<Player>().PlayerID = i;        

            Vector2 viewRect = Globals.ReturnCorrectCameraRect(i);
            AllPlayersInMatch[i].GetComponentInChildren<Camera>().rect = new Rect(viewRect[0], viewRect[1], 0.5f, 0.5f);
            AllPlayersInMatch[i].FindGrandchild("Death Camera").GetComponent<Camera>().rect = new Rect(viewRect[0], viewRect[1], 0.5f, 0.5f);
        }
    }

    void SetUpTeamSpawnPositions()
    {
        for (byte i = 0; i < Teams.Length; i++)
        {
            if (Teams[i].SpawnPositions == null) Teams[i].SpawnPositions = new Transform[AllPlayersInMatch.Count / 2];

            for (byte a = 0; a < Teams[i].SpawnPositions.Length; a++)
                Teams[i].SetUpTeamSpawns(a, GameObject.Find("Team Spawns").transform.GetChild(Teams[i].TeamNumber - 1).GetChild(a));
        }
    }

    void AssignTeamNumberToPlayers()
    {

        for (byte i = 0; i < AllPlayersInMatch.Count; i++)
        {
            if (AllPlayersInMatch[i].GetComponent<Player>().PlayerID == 0 || AllPlayersInMatch[i].GetComponent<Player>().PlayerID == 2)
                AllPlayersInMatch[i].GetComponent<Player>().SetTeamNumber(1);
            else
                AllPlayersInMatch[i].GetComponent<Player>().SetTeamNumber(2);
        }
    }

    void CreatePlayers()
    {
        for (byte i = 0; i < 4; i++)
            Instantiate(Resources.Load("Prefabs/Testing/Test Machi") as GameObject);
    }

    void SetUpPlayersForBattle()
    {
        AllPlayersInMatch.ForEach(x => x.parent = null);
        Destroy(GameObject.Find("Players"));

        SetPlayerID();
        AssignTeamNumberToPlayers();
    }

    #region Methods to be called at the beginning of a round.
    /// <summary>
    /// Repositions all of the players back to their correct spawns.
    /// </summary>
    private void RepositionAllPlayersToRespectiveSpawns()
    {
        for(byte i = 0; i < Teams.Length; i++)
        {
            foreach (GameObject p in Teams[i].TeamMembers)
                ActivateSpawn(p);
        }
    }

    // Activates the player's spawn.
    private void ActivateSpawn(GameObject player)
    {
        int teamIndexToCheck = player.GetComponent<Player>().TeamNumber - 1;

        player.GetComponent<Player>().SetPlayerDefaults();

        for (byte i = 0; i < Teams[teamIndexToCheck].TeamMembers.Count; i++)
        {
            if (player == Teams[teamIndexToCheck].TeamMembers[i])
                player.transform.position = Teams[teamIndexToCheck].SpawnPositions[i].position;
        }
    }
    #endregion

    private void SetPlayerColors()
    {
        for(byte i = 0; i < Teams.Length; i++)
        {
            foreach (GameObject p in Teams[i].TeamMembers)
                if(p.name.Contains("Test")) p.GetComponent<Player>().ChangeBodyColor(Teams[i].TeamColor);
        }
    }
    #endregion


    #region Methods called each round.
    private void BeginGame()
    {
        if(CurrentRoundState == RoundState.IN_PROGRESS && MatchCurrentlyInProgress() || CurrentRoundState == RoundState.NOT_STARTED)
        {
            if (CurrentRoundState != RoundState.NOT_STARTED) AdvanceGameToCorrectRound(); 
            StartCoroutine(StartRound());
        }
    }

    private void AdvanceGameToCorrectRound()
    {
        if(
            ((Teams[0].Score > Teams[1].Score) && CurrentMatchProgress - 1 != Teams[0].Score) || 
            ((Teams[1].Score > Teams[0].Score) && CurrentMatchProgress - 1 != Teams[1].Score)
          )
        {
            Debug.Log("Round has advanced...");
            CurrentMatchProgress++;
        }
        else
        {
            Debug.Log("Round stayed the same.");
        }
    }

    /// <summary>
    /// Starts the next round.
    /// </summary>
    private IEnumerator StartRound()
    {
        //for (byte i = 0; i < Teams.Length; i++)
        //    Teams[i].TeamMembers.ForEach(x => Teams[i].ActiveTeamMembers.Add(x));

        RepositionAllPlayersToRespectiveSpawns();
        ChangeControllableStateOfPlayers(InterfacingState.SPECTATING);

        yield return new WaitForSeconds(RoundStartTime);
        Debug.Log("Begin round " + CurrentMatchProgress);
        ChangeControllableStateOfPlayers(InterfacingState.CONTROLLABLE);

        //CurrentRoundState = RoundState.IN_PROGRESS;
    }

    /// <summary>
    /// Ends the match.
    /// </summary>
    private void EndMatch()
    {
        Debug.Log("Match ended.");
    }

    private void MakeAllPlayersControllable()
    {
        for (byte i = 0; i < AllPlayersInMatch.Count; i++)
        {
            AllPlayersInMatch[i].gameObject.SetActive(false);
            AllPlayersInMatch[i].gameObject.SetActive(true);
            AllPlayersInMatch[i].GetComponent<Player>().CurrentInterfacingState = InterfacingState.CONTROLLABLE;
        }
    }

    private void ChangeControllableStateOfPlayers(InterfacingState state, Transform specificPlayer = null)
    {
        if(specificPlayer != null)
        {
            specificPlayer.gameObject.SetActive(false);
            specificPlayer.gameObject.SetActive(true);
            specificPlayer.GetComponent<Player>().CurrentInterfacingState = state;
        }
        else
        {
            foreach (Transform t in AllPlayersInMatch)
            {
                t.gameObject.SetActive(false);
                t.gameObject.SetActive(true);
                t.GetComponent<Player>().CurrentInterfacingState = state;
            }
                
        }
    }
    #endregion

    // Continually checks if any of the entities are dead.
    private void CheckIfAnyLiveEntitiesAreDead()
    {
        for(byte i = 0; i < Teams.Length; i++)
        {
            if (Teams[i].AllPlayersDowned())
            {
                CurrentRoundState = RoundState.ENDED;
                ChangeTeamScore(i);
            }
        }
    }

    private void ChangeTeamScore(int teamDowned)
    {
        string team = "0";

        if(teamDowned == 0)
        {
            team = "2";
            Teams[1].AddToScore(1);
        }
        else
        {
            team = "1";
            Teams[0].AddToScore(1);
        }

        Debug.Log(string.Format("Team {0} gained a point.", team));
    }

    bool MatchCurrentlyInProgress() { return CurrentMatchProgress < 5; }

#if UNITY_EDITOR
    private void PerformScoreDebugging()
    {
        int teamThatScored = -1;

        if(Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.P))
        {
            if (Input.GetKeyDown(KeyCode.K))
                teamThatScored = 1;
            else if (Input.GetKeyDown(KeyCode.P))
                teamThatScored = 2;

            if (Teams[teamThatScored - 1].TeamMembers[0].GetComponent<DamageableObject>().Health > 0) Teams[teamThatScored - 1].TeamMembers[0].GetComponent<DamageableObject>().Health = 0;
            else
                Teams[teamThatScored - 1].TeamMembers[1].GetComponent<DamageableObject>().Health = 0;
        }
    }
#endif    
}
