using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : InstancedManager
{

    new void Awake()
    {
        base.Awake();
        _currentGameState = GameState.CHARACTER_SELECT;

        GlobalManagement.ReportEndOfScene(_currentGameState);
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
