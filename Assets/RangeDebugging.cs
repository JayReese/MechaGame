using UnityEngine;
using System.Collections;

public class RangeDebugging : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	    for(byte i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Contains("Ground"))
                transform.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.red;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
