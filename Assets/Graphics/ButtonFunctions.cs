using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField]
    GameObject CreditsPanel;

    [SerializeField]
    GameObject LoadingPanel;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void StartButtonPushed()
    {
        LoadingPanel.SetActive(true);
        //Do the load
        Debug.Log("Startbutton Pushed");
    }

    public void CreditsButtonPushed()
    {
        CreditsPanel.SetActive(true);
        Debug.Log("Creditsbutton Pushed");
    }

    public void ExitButtonPushed()
    {
        Application.Quit();
        Debug.Log("Exitbutton Pushed");
    }
}
