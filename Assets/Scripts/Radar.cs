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

    public void ActivateRadar()
    {
        Debug.Log("Radar activated");
        StartCoroutine(ToggleRadar());
    }

    IEnumerator ToggleRadar()
    {
        GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider>().enabled = false;
    }

    void OnTriggerEnter(Collider c)
    {
        Debug.Log(c.name);
    }
}
