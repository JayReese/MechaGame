using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDebugging : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward * Time.deltaTime;
	}

    void OnTriggerEnter(Collider c)
    {
        Debug.Log(c.name);
    }
}
