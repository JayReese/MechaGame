using UnityEngine;
using System.Collections;
using System;

public class ArmorPiece : DamageableObject
{
    [SerializeField]
    int CurrentStructural;

    public override void ReceiveDamage(int amount)
    {
        base.ReceiveDamage(amount);
        Debug.Log(string.Format("damage dealt to {0}, {1} HP left.", gameObject.name, Health));
    }

    // Use this for initialization
    void Start ()
    {
       
        Health = 10;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //CurrentStructural = StructuralIntegrity;
        //CheckIfDestroyed();
	}

    private void CheckIfDestroyed()
    {
        Health = Health <= 0 ? 0 : Health;

        if (Health == 0)
        {
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }
            
    }

    protected override void OnEnable()
    {
        IsPersistingObject = false;
        DamageSurfaceType = SurfaceType.ARMOR;

        base.OnEnable();
    }
}
