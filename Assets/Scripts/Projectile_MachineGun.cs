using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_MachineGun : CollisionBasedProjectile
{
    #region Overloaded methods.
    new void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    new void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        //if (Globals.RaycastHitTarget(transform.position, transform.forward, 2f).transform != null)
        //    Debug.Log("You hit " + Globals.RaycastHitTarget(transform.position, transform.forward, 2f).transform.name);
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    new void OnEnable()
    {
        base.OnEnable();
    }
    #endregion

    new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
