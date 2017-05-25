using UnityEngine;
using System.Collections;
using System;

public class LiveEntity : MonoBehaviour, IDamageable
{
    public int Health;

    /// <summary>
    /// From the IDamageable interface, which allows an object to take damage. In this context, all LiveEntities (entities with some form of AI/controller) will be harmed by things that call this function.
    /// </summary>
    /// <param name="amount"></param>
    public void ReceiveDamage(int amount)
    {
        Health -= amount;
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
