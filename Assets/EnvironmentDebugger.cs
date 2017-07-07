using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDebugger : MonoBehaviour
{
    Transform player;
    float respawnTestTimer;

	// Use this for initialization
	void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        respawnTestTimer = 10f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            player.GetComponent<DamageableObject>().Health = player.GetComponent<DamageableObject>().Health > 0 ? 0 : 10;
            
	}
}
