using UnityEngine;
using System.Collections;
using System;

public class Radar : MonoBehaviour
{


	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void Activate()
    {
        StartCoroutine(ActivateRadarSystem());
    }

    IEnumerator ActivateRadarSystem()
    {
        GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Collider>().enabled = false;
    }
}
