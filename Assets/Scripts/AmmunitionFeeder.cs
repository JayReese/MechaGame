using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmmunitionFeeder
{
    //List<GameObject> ProjectileObjectPool;
    //public int test;

    public AmmunitionFeeder(ProjectileStatConfig projectileStats, Transform ammoParent)
    {
        //ProjectileObjectPool = new List<GameObject>();
        CreateAmmoPool(projectileStats, ammoParent);
    }

    public void CreateAmmoPool(ProjectileStatConfig projectileStats, Transform newParent)
    {
        GameObject g = MonoBehaviour.Instantiate(projectileStats.ProjectilePrefab);

        for(int i = 0; i < projectileStats.MagazineSize; i++)
        {
            g.GetComponent<Projectile>().FlightSpeed = projectileStats.ProjectileFlightSpeed;
            g.GetComponent<Projectile>().BaseDamage = projectileStats.BaseDamageDealt;
            g.GetComponent<Projectile>().ArmorInteractionValue = projectileStats.ArmorInteractionValue;
            g.GetComponent<Projectile>().LockOnHardnessValue = projectileStats.LockOnHardnessValue;
            g.GetComponent<Projectile>().WeaponOrigin = projectileStats.WeaponOrigin;
            g.transform.parent = newParent;
            g.SetActive(false);
        }
    }
}
