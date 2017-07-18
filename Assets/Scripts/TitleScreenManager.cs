using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : InstancedManager
{
    new void Awake()
    {
        base.Awake();
    }

	// Use this for initialization
	void Start ()
    {
        _currentGameState = GameState.MAIN_MENU;
        //GlobalManagement.ReportEndOfScene(_currentGameState);

        GlobalManagement.ReportEndOfScene(_currentGameState);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
