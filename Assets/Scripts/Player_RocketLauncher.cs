using UnityEngine;
using System.Collections;

public class Player_RocketLauncher : Player
{

    new void Awake()
    {
        base.Awake();
    }

	// Use this for initialization
	new void Start ()
    {
        FirstSubWeaponCooldown = 8f;
        SecondSubWeaponCooldown = 8f;

        Health = 10;

        base.Start();
    }
	
	// Update is called once per frame
	new void Update ()
    {
        base.Update();
	}
}
