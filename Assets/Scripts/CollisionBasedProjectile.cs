using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBasedProjectile : Projectile
{
    #region Overloaded methods.
    new void Awake()
    {
        base.Awake();
    }

	// Use this for initialization
	new void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	new void Update ()
    {
        base.Update();
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
}
