using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testbattle : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward * Time.deltaTime * 20f;
	}
}
