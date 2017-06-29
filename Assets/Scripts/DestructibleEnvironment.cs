using UnityEngine;
using System.Collections;

public class DestructibleEnvironment : DamageableObject
{
    private void Awake()
    {
        IsPersistingObject = false;
        IsPlayer = false;
    }

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
