using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    MenuInputScheme MenuInput;

	// Use this for initialization
	void Start ()
    {
        MenuInput = GetComponent<MenuInputScheme>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (MenuInput.SelectionCyclingAxis > 0)
            Debug.Log(MenuInput.SelectionCyclingAxis);
	}
}
