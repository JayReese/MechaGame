using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpiderDrone : Deployable
{
    Vector3 TargetDestination;    

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnEnable()
    {
        Health = 5;
        IsPersistingObject = false;
    }

    void GetWayPoint()
    {

    }
}
