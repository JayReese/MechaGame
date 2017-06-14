using UnityEngine;
using System.Collections;

public class TestingCharacter : MonoBehaviour
{

    public Color[] TeamColors;

	// Use this for initialization
	void Awake ()
    {
        SetTeamColors();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //GetComponent<MeshRenderer>().material.color = new Color(171 / 255, 46 / 255, 59 / 255);
	}

    void SetTeamColors()
    {
        TeamColors = new Color[2];
        TeamColors[0] = new Color(255 / 255, 251 / 255, 68 / 255);
        TeamColors[1] = new Color(124 / 255, 25 / 255, 255 / 255);
    }
}
