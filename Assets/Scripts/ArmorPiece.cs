using UnityEngine;
using System.Collections;
using System;

public class ArmorPiece : DamageableObject
{
    public override void ReceiveDamage(int amount)
    {
        base.ReceiveDamage(amount);
        
    }

    void Start()
    {
        Health = 10;
    }

	// Update is called once per frame
	void Update ()
    {
        CheckIfDestroyed();
	}

    private void CheckIfDestroyed()
    {
        Health = Health <= 0 ? 0 : Health;
    }

    void OnEnable()
    {
        IsPersistingObject = false;
        DamageSurfaceType = SurfaceType.ARMOR;
    }

    public void SetUniqueParameters(int newHealth)
    {
        Health = newHealth;
    }
}
