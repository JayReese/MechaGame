using UnityEngine;
using System.Collections;

public class Weapon_MachineGunner : Weapon
{

	// Use this for initialization
	new void Start ()
    {
        MaxMagazineSize = 10;

        ReloadSpeed = 1.0f;
        FireRate = 1.5f;

        BurstCount = 5;
        ShotInterval = 0.4f;

        LockOnHardnessValue = 2;
        base.Start();
	}
	
	// Update is called once per frame
	new void Update ()
    {
        base.Update();
	}
}
