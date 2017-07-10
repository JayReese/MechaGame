using UnityEngine;
using System.Collections;

public class Deployable : DamageableObject
{

	// Use this for initialization
	void Start ()
    {
        Health = 5;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnEnable()
    {
        IsPersistingObject = false;
    }
}
