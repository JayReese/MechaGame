using UnityEngine;
using UnityEngine.UI;

public class TeamScore : MonoBehaviour {
    [SerializeField]
    Text Team1Score;
    [SerializeField]
    Text Team2Score;

    public int Team1Kills = 0;
    public int Team2Kills = 0;
    // Use this for initialization
    void Start () {
        Team1Score.text = string.Format("Team 1 Score: 0");
        Team2Score.text = string.Format("Team 2 Score: 0");
    }
	
	// Update is called once per frame
	void Update () {
        if (Team1Kills >= 10)
        {
            Debug.Log("Team 1 Wins");
        }
        else if (Team2Kills >= 10)
        {
            Debug.Log("Team 2 Wins");
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Team1Kills++;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Team2Kills++;
        }

        Team1Score.text = string.Format("Team 1 Score: {0}",Team1Kills);
        Team2Score.text = string.Format("Team 2 Score: {0}",Team2Kills);
    }
}
