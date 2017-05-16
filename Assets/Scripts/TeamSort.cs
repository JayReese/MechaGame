using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamSort : MonoBehaviour {
    [SerializeField]
    Slider teamSlider;
    [SerializeField]
    GameObject team1Member;
    [SerializeField]
    GameObject team2Member;
    [SerializeField]
    Canvas teamSelectCanvas;
    [SerializeField]
    Camera teamSelectCamera;
    public Transform team1Spawn;
    public Transform team2Spawn;

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
        if (teamSlider.value <= .5)
        {
            Instantiate(team2Member, team2Spawn.position, Quaternion.identity);
            teamSelectCanvas.enabled = false;
            teamSelectCamera.enabled = false;
        }
        if (teamSlider.value > .5)
        {
            Instantiate(team1Member, team1Spawn.position, Quaternion.identity);
            teamSelectCanvas.enabled = false;
            teamSelectCamera.enabled = false;
        }
    }
	
}
