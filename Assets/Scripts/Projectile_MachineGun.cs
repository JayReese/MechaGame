using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_MachineGun : CollisionBasedProjectile
{

    // Use this for initialization

    new void Start ()
    {
		
	}
	
	// Update is called once per frame
	new void Update ()
    {
        if (Globals.RaycastHitTarget(transform.position, transform.forward, 2f).transform != null)
            Debug.Log("You hit " + Globals.RaycastHitTarget(transform.position, transform.forward, 2f).transform.name);
    }
}
