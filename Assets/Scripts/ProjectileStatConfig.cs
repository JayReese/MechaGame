using UnityEngine;
using System.Collections;

public class ProjectileStatConfig
{
    public int MagazineSize { get; set; }
    public GameObject ProjectilePrefab { get; set; }
    public float ProjectileFlightSpeed { get; set; }
    public int BaseDamageDealt { get; set; }
    public int LockOnHardnessValue { get; set; }
    public ArmorPiercingInteraction ArmorInteractionValue { get; set; }

    public Transform WeaponOrigin { get; set; }
}
