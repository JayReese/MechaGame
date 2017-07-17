using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    /* 
     * The purpose of this class is to KEEP TRACK of the current GAME STATE and to CHANGE SCENES if the state changes
     * There should only ever be one instance of this class
     */

    [SerializeField] string mainMenuSceneName;
    [SerializeField] string characterSelectSceneName;
    [SerializeField] string mainGameSceneName;
    [SerializeField] string endOfMatchSceneName;

    public static StateManager instance = null; //you can make this private for the stateManager to be a "private singleton"

    
    static GameState currGameState;
    static GameState prevGameState;

    public GameState currentGameState
    {
        get { return currGameState; }
        set
        {
            prevGameState = currGameState;
            currGameState = value;
            ChangeSceneOnStateChange();
        }
    }

    public GameState previousGameState { get { return prevGameState; } }

    //Singleton... at least, the Unity version of one
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    //Called inside "currentGameState" property
    void ChangeSceneOnStateChange()
    {
        switch (currentGameState)
        {
            case (GameState.MAIN_MENU):
                SceneManager.LoadScene("TitleScreen");
                break;
            case (GameState.CHARACTER_SELECT):
                SceneManager.LoadScene("CharacterSelection");
                break;
            case (GameState.MAIN_GAME):
                SceneManager.LoadScene("Battlefield");
                break;
            case (GameState.END_OF_MATCH):
                SceneManager.LoadScene("ReturnScreen");
                break;
        }
    }
}