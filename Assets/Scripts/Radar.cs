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
        //StartCoroutine(ActivateRadarSystem());
        ActivateRadar();
    }

    //IEnumerator ActivateRadarSystem()
    //{
        
    //}

    void ActivateRadar()
    {
        GetComponent<Collider>().enabled = true;

        GetComponent<Collider>().enabled = false;
    }
}
