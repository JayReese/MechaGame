using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TitleMenu : MonoBehaviour {

	[SerializeField]
	public List<Button> Buttons = new List<Button>();
	public int Selection;
	public string selectionInput="Vertical";
	public float selectionSensitivity;


	// Use this for initialization
	void Start () 
	{
		Selection = 0;

	}
	
	// Update is called once per frame
	void Update ()
	{
		Buttons [Selection].Select ();
			
		if (Input.GetButtonDown(selectionInput)) 
		{
			if (Input.GetAxis (selectionInput) > 0) 
			{
				Selection--;
			}

			if (Input.GetAxis (selectionInput) < 0) 
			{
				Selection++;
			}

			if (Selection < 0) 
			{
				Selection = 2;
			}
			if (Selection > 2) 
			{
				Selection = 0;
			}
		}
		//if(Input.GetKeyDown(KeyCode.

	}
}
