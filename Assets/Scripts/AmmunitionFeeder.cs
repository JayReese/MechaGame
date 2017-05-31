using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmmunitionFeeder
{
    List<GameObject> ProjectileObjectPool;
    public int test;

    public AmmunitionFeeder(ProjectileStatConfig projectileStats)
    {
        ProjectileObjectPool = new List<GameObject>();
        CreateAmmoPool(projectileStats);
    }

    public void CreateAmmoPool(ProjectileStatConfig projectileStats)
    {
        test = projectileStats.LockOnHardnessValue;
    }
}
