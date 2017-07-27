using UnityEngine;
using System.Collections;

public class Weapon_MachineGunner : Weapon
{

    public float MagSize
    {
        get
        {
            return MaxMagazineSize;
        }

        private set
        {
            MaxMagazineSize = (int)value;
        }
    }
    public float RemainingAmmunition
    {
        get
        {
            return CurrentMagazineSize;
        }

        private set
        {
            CurrentMagazineSize = (int)value;
        }
    }
    
    // Use this for initialization
    new void Start ()
    {
        MaxMagazineSize = 14;

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
