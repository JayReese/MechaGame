using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldUIManager : MonoBehaviour
{
    public enum MatchUIState { CHARACTER_SELECT, IN_GAME, RESULTS_SCREEN }

    [SerializeField]
    MatchUIState _currentMatchUIState, _previousMatchUIState;

    [SerializeField] GameObject[] InGameUIElements;

    public MatchUIState CurrentMatchUIState
    {
        get { return _currentMatchUIState; }
        set
        {
            _previousMatchUIState = _currentMatchUIState;
            _currentMatchUIState = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        InGameUIElements = GameObject.FindGameObjectsWithTag("Game UI");
        _currentMatchUIState = MatchUIState.CHARACTER_SELECT;
	}
	
	// Update is called once per frame
	void Update ()
    {
#if UNITY_EDITOR
        if (GetComponent<GameModeManager>().MatchCurrentlyInProgress()) PerformMatchUIStateDebugging();
#endif

        ShiftGameUI();
    }

    public void ShiftGameUI()
    {
        if (_currentMatchUIState != _previousMatchUIState)
            Debug.Log("UI manager changed");
    }

#if UNITY_EDITOR
    private void PerformMatchUIStateDebugging()
    {
        if (Input.GetKeyDown(KeyCode.T))
            _currentMatchUIState = (MatchUIState)(((int)_currentMatchUIState) + 1);

        Debug.Log(CurrentMatchUIState);
    }
#endif
}
