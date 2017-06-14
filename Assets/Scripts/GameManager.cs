using UnityEngine;
using System.Collections;

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
    [SerializeField] GameObject[] mechaPlayerPrefabs;
    [SerializeField] TeamTransformPositions[] TeamSpawnPositions;
    [SerializeField]
    GameObject[] TeamSpawningPositions;

    public const int pointsPerKill = 100;       //how many points the team will score when they get a kill
    const int numberOfTeams = 2;                //this variable can be adjusted in the future if we ever want more than 2 teams
    int NumPlayers;
    int[] teamScoreboards;

    GameObject[] Players;

    //You can't have 2D arrays exposed to the editor, so this is the workaround
    [System.Serializable]
    struct TeamTransformPositions
    {
        public Transform[] positions;
    }


    void Awake()
    {
        //CreatePlayers();
        Players = GameObject.FindGameObjectsWithTag("Controllable");
        //determine how many controllers are hooked up
        NumPlayers = Input.GetJoystickNames().Length;
        //Debug.Log(players);
        teamScoreboards = new int[NumPlayers];
        //bool unevenTeams = (players % numberOfTeams != 0);    //this variable can be used to determine if the teams cannot be evenly distributed
        SetPlayerID();
        //SetUpPlayerSpawns();
    }
	// Use this for initialization
	void Start ()
    {

	}

    void SetPlayerID()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].GetComponent<Player>().PlayerID = i;
            //Players[i].GetComponent<Player>().SetTeamNumber(i + 1 < 3 ? 1 : 2);
           
            Vector2 viewRect = Globals.ReturnCorrectCameraRect(i);
            Players[i].GetComponentInChildren<Camera>().rect = new Rect(viewRect[0], viewRect[1], 0.5f, 0.5f);
        }

        #region Commented out.
        //int playerNum = 0;
        //while (playerNum < NumPlayers) //loop through all the players
        //{
        //    for (int teamNum = 0; teamNum < numberOfTeams; teamNum++) //divide the players into teams
        //    {
        //        //GameObject newPlayerMech = Instantiate(mechaPlayerPrefabs[0]);
        //        //newPlayerMech.GetComponent<DeleteMeTempController>().FirstTimeSetup(this, teamNum, TeamSpawnPositions[teamNum].positions);

        //        playerNum++;
        //        Debug.Log("Player: " + playerNum + " has been placed in team " + teamNum);

        //        if (playerNum >= NumPlayers) //if we've matched or exceeded our number of players, stop looping
        //        {
        //            break;
        //        }

        //    }
        //}
        #endregion
    }


    public void AdjustTeamPoints(int amount, int teamIndex)
    {
        teamScoreboards[teamIndex] += amount;

        Debug.Log("Team " + teamIndex + " now has " + teamScoreboards[teamIndex] + " points");

        if(teamScoreboards[teamIndex] >= pointsToWinThreshhold)
        {
            ActivateWin();
        }
    }
    
    void SetUpPlayerSpawns()
    {
        TeamSpawningPositions = GameObject.FindGameObjectsWithTag("Spawner");
        int a = 0;
        int b = 0;

        foreach(GameObject p in Players)
        {
            if (p.GetComponent<Player>().TeamNumber == 1)
            {
                p.GetComponent<Player>().SetUpPlayerSpawn(
                    TeamSpawningPositions[p.GetComponent<Player>().TeamNumber - 1].transform.GetChild(a).InverseTransformPoint(TeamSpawningPositions[p.GetComponent<Player>().TeamNumber - 1].transform.GetChild(a).position)
                    );
                a++;
            }    
            else if(p.GetComponent<Player>().TeamNumber == 2)
            {
                p.GetComponent<Player>().SetUpPlayerSpawn(TeamSpawningPositions[p.GetComponent<Player>().TeamNumber - 1].transform.GetChild(b).InverseTransformPoint(TeamSpawningPositions[p.GetComponent<Player>().TeamNumber - 1].transform.GetChild(b).position));
                b++;
            }
                
        }
    }

    void CreatePlayers()
    {
        for (byte i = 0; i < 4; i++)
            Instantiate(Resources.Load("Prefabs/Testing/Test Machi") as GameObject);
    }


    void ActivateWin()
    {
        //do more stuff (like register scores, calculate MVP, send info to the next scene, signal the UI to update/display end of match stats, etc)

        StateManager.instance.currentGameState = StateManager.GameState.END_OF_MATCH;
    }
}
