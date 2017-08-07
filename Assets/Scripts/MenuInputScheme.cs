using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputScheme : MonoBehaviour
{
    public float SelectionCyclingAxis { get; private set; }

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        SelectionCyclingAxis = Input.GetAxisRaw("Vertical");
        //Debug.Log(SelectionCyclingAxis);
	}
}
