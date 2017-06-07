using UnityEngine;
using System.Collections;
using System;

public class ArmorPiece : DamageableObject
{
    [SerializeField] int CurrentStructural;
    public int StructuralIntegrity { get; private set; }


    public override void ReceiveDamage(int amount)
    {
        StructuralIntegrity -= amount;
        Debug.Log(string.Format("damage dealt to {0}, {1} HP left.", gameObject.name, StructuralIntegrity));

        if (StructuralIntegrity <= 0)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        IsPersistingObject = false;
        StructuralIntegrity = 10;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CurrentStructural = StructuralIntegrity;
        //CheckIfDestroyed();
	}

    private void CheckIfDestroyed()
    {
        StructuralIntegrity = StructuralIntegrity <= 0 ? 0 : StructuralIntegrity;

        if (StructuralIntegrity == 0)
        {
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }
            
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
}
