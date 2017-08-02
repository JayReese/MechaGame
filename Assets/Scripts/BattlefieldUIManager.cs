using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldUIManager : MonoBehaviour
{
    public enum MatchUIState { CHARACTER_SELECT = 1, ROUND_STARTING, RESULTS_SCREEN }

    [SerializeField]
    MatchUIState _currentMatchUIState;
    [SerializeField]
    bool _setUIToChange;

    [SerializeField] GameObject GlobalUIElement;

    public MatchUIState CurrentMatchUIState
    {
        get { return _currentMatchUIState; }
    }

    // Use this for initialization
    void Start ()
    {
        GlobalUIElement = GameObject.FindGameObjectWithTag("Game UI");
        _currentMatchUIState = MatchUIState.CHARACTER_SELECT;
	}
	
	// Update is called once per frame
	void Update ()
    {
#if UNITY_EDITOR
        if (GetComponent<GameModeManager>().MatchCurrentlyInProgress()) PerformMatchUIStateDebugging();
#endif

        //ShiftGameUI();
    }

#if UNITY_EDITOR
    private void PerformMatchUIStateDebugging()
    {
        bool buttonPress = Input.GetKeyDown(KeyCode.T);

        if (buttonPress && Enum.IsDefined(typeof(MatchUIState), ((int)_currentMatchUIState) + 1))
        {
            ActivateCorrectGlobalUIElement(((int)_currentMatchUIState) + 1);
            _currentMatchUIState = (MatchUIState)(((int)_currentMatchUIState) + 1);
        }
    }
#endif

    private void ChangeGlobalUIState()
    {
        if (Enum.IsDefined(typeof(MatchUIState), ((int)_currentMatchUIState) + 1))
        {
            ActivateCorrectGlobalUIElement(((int)_currentMatchUIState) + 1);
            _currentMatchUIState = (MatchUIState)(((int)_currentMatchUIState) + 1);
        }
    }

    private void ActivateCorrectGlobalUIElement(int uiToActivate, bool deactivateGlobalUIElements = false)
    {
        if(deactivateGlobalUIElements)
        {
            GlobalUIElement.SetActive(true);

            for (int i = 0; i < GlobalUIElement.transform.childCount; i++)
                GlobalUIElement.transform.GetChild(i).gameObject.SetActive(i == uiToActivate);
        }
        else
            GlobalUIElement.SetActive(false);
    }
}
