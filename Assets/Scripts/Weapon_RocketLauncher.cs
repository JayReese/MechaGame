using UnityEngine;
using System.Collections;

public class Weapon_RocketLauncher : Weapon
{

	// Use this for initialization
	new void Start ()
    {
        MaxMagazineSize = 5;

        ReloadSpeed = 1.0f;
        FireRate = 1.5f;

        BurstCount = 1;
        ShotInterval = 0;

       

        LockOnHardnessValue = 2;
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
