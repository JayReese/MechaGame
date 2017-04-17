using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] int NumberOfPlayers;
    [SerializeField] CameraRects Rects;

	// Use this for initialization
	void Start ()
    {
        //NumberOfPlayers = NumberOfPlayers >= 3 && NumberOfPlayers > 1 ? 4 : 2;
        NumberOfPlayers = 2;
        Rects = new CameraRects();
        CreatePlayer();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void CreatePlayer()
    {
        for (byte i = 0; i < NumberOfPlayers; i++)
        {
            InstantiateNewPlayer(i + 1);
        }
    }

    void InstantiateNewPlayer(int id)
    {
        GameObject np = Resources.Load("Prefabs/Mecha/Test Player") as GameObject;

        np.GetComponent<Player>().PlayerID = id;

        np.transform.FindChild("Camera").GetComponent<Camera>().rect = NumberOfPlayers == 4 ? 
            new Rect(Rects.FourPlayerCamRects[id - 1][0], Rects.FourPlayerCamRects[id - 1][1], Rects.FourPlayerCamRects[id - 1][2], 0.5f) : 
            new Rect(Rects.TwoPlayerCamRects[id - 1][0], Rects.TwoPlayerCamRects[id - 1][1], 1, 0.5f);

        Instantiate(np);
    }
}