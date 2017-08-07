    using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class  CharacterSelectScript:MonoBehaviour {	// Use this for initialization


	[SerializeField]
	public List<Button> Buttons = new List<Button>();
	public int Selection;
	public string selectionInput="Horizontal";
	public float selectionSensitivity;
	public List<Texture2D>Characters=new List<Texture2D>();



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
				if (Input.GetAxis (selectionInput) < 0) 
				{
					Selection--;
				}

				if (Input.GetAxis (selectionInput) > 0) 
				{
					Selection++;
				}

				if (Selection < 0) 
				{
					Selection = 6;
				}
				if (Selection > 6) 
				{
					Selection = 0;
				}
			}
			
		}
	}
