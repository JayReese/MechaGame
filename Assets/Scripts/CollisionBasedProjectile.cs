using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBasedProjectile : Projectile
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }
}
