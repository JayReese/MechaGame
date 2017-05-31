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

    public const int pointsPerKill = 100;       //how many points the team will score when they get a kill
    const int numberOfTeams = 2;                //this variable can be adjusted in the future if we ever want more than 2 teams

    int[] teamScoreboards;

    //You can't have 2D arrays exposed to the editor, so this is the workaround
    [System.Serializable]
    struct TeamTransformPositions
    {
        public Transform[] positions;
    }

	// Use this for initialization
	void Start ()
    {
        //determine how many controllers are hooked up
        int players = Input.GetJoystickNames().Length;
        teamScoreboards = new int[players];
        //bool unevenTeams = (players % numberOfTeams != 0);    //this variable can be used to determine if the teams cannot be evenly distributed

        int playerNum = 0;
        while(playerNum < players) //loop through all the players
        {
            for (int teamNum = 0; teamNum < numberOfTeams; teamNum++) //divide the players into teams
            {
                GameObject newPlayerMech = Instantiate(mechaPlayerPrefabs[0]);
                newPlayerMech.GetComponent<DeleteMeTempController>().FirstTimeSetup(this, teamNum, TeamSpawnPositions[teamNum].positions);

                playerNum++;
                Debug.Log("Player: " + playerNum + " has been placed in team " + teamNum);

                if (playerNum >= players) //if we've matched or exceeded our number of players, stop looping
                {
                    break;
                }
                
            }
        }
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


    void ActivateWin()
    {
        //do more stuff (like register scores, calculate MVP, send info to the next scene, signal the UI to update/display end of match stats, etc)

        StateManager.instance.currentGameState = StateManager.GameState.END_OF_MATCH;
    }
}
