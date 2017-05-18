/*
Requires that you place all prefabs into scene and connect them through serialized fields.
This is temporary as I will change it to where on awake it finds all the necessary components on its own.
Required prefabs in scene:
-GUI
-TeamSelectUI
-TeamManager
-TeamSelectManager
Other Requirements:
-two empty gameObjects to serve as spawn points for Team1 and Team2 prefab
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TeamSort : MonoBehaviour {
    //Prefabs to load
    [SerializeField]
    GameObject team1Member;
    [SerializeField]
    GameObject team2Member;
    //UI
    [SerializeField]
    Slider teamSlider;
    [SerializeField]
    Canvas teamSelectCanvas;
    [SerializeField]
    Canvas GUI;
    [SerializeField]
    Camera teamSelectCamera;
    //Spawns
    public Transform team1Spawn;
    public Transform team2Spawn;
    //Player array
    public TempPlayer[] players;

    void Awake()
    {
        GUI.enabled = false;
    }

    public void SetTeamSliderPosition()
    {
        if (teamSlider.value > .5)
        {
            teamSlider.value = 0;
        }
        else
        {
            teamSlider.value = 1;
        }
    }

    public void SpawnTeamMember()
    {
        //How team membbers are spawned to a specific team
        GameObject temp;
        if (teamSlider.value >= .5)
        {       
            temp = Instantiate(team2Member, team2Spawn.position, Quaternion.identity) as GameObject;
            temp.gameObject.tag = "Team2";
            teamSelectCanvas.enabled = false;
            teamSelectCamera.gameObject.SetActive(false);
            GUI.enabled = true;
        }
        else if (teamSlider.value < .5)
        {
            temp = Instantiate(team1Member, team1Spawn.position, Quaternion.identity) as GameObject;
            temp.gameObject.tag = "Team1";
            teamSelectCanvas.enabled = false;
            teamSelectCamera.gameObject.SetActive(false);
            GUI.enabled = true;
        }
        //Sets the parent of the TempPlayer to the appropriate Team(emptyGameObjects from TeamManagerPrefab)
        players = FindObjectsOfType(typeof(TempPlayer)) as TempPlayer[];
        for (int i = 0; i < players.Length; i++)
        {
            //this only works if the tags are premade
            if (players[i].gameObject.tag == "Team1")
            {
                players[i].gameObject.transform.SetParent(FindObjectOfType<TeamScore>().transform.GetChild(0));
            }
            if (players[i].gameObject.tag == "Team2")
            {
                players[i].gameObject.transform.SetParent(FindObjectOfType<TeamScore>().transform.GetChild(1));
            }
        }

    }
	
}
