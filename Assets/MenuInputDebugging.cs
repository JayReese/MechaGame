using UnityEngine;
using System.Collections;

public class MenuInputDebugging : MonoBehaviour {


	// Use this for initialization
	void Start () {
        
	
	}
    int CurrentMenuChoice = 1;
    void GoToNextOption()
    {
        

        CurrentMenuChoice++;

        if (CurrentMenuChoice >= 5)
            CurrentMenuChoice = 1;
    }

    void ReportCurrentChoice()
    {
        Debug.Log(string.Format("Currently selected is {0}", CurrentMenuChoice ));
    }
	
	// Update is called once per frame
	void Update () {
	if (Input.GetAxisRaw("Vertical") == 1)
        {
            //Debug.Log();
            
            GoToNextOption();
        }

        Debug.Log(string.Format("Currently selected is {0}", CurrentMenuChoice));
    }
}

