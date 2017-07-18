using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] StateManager GlobalStateManagement;
    byte[] PlayerPrefabDataSaved;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start ()
    {
        GlobalStateManagement = GetComponent<StateManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ReportEndOfScene(GameState currentGameState)
    {
        GlobalStateManagement.ChangeState((GameState)((int)currentGameState + 1));
    }
}
