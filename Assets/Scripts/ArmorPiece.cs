using UnityEngine;
using System.Collections;
using System;

public class ArmorPiece : MonoBehaviour, IDamageable
{
    public int StructuralIntegrity { get; private set; }

    public void ReceiveDamage(int amount)
    {
        StructuralIntegrity -= amount;
    }

    // Use this for initialization
    void Start ()
    {
        StructuralIntegrity = 10;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckIfDestroyed();
	}

    private void CheckIfDestroyed()
    {
        if (StructuralIntegrity == 0)
            Destroy(gameObject);
    }

    public void Degrade(int amount)
    {
        StructuralIntegrity -= amount;
        StructuralIntegrity = StructuralIntegrity <= 0 ? 0 : StructuralIntegrity;
    }

    
}
