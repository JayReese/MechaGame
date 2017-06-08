using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    public int LockOnHardnessValue, ArmorInteractionValue;
    public Transform WeaponOrigin, PlayerOrigin, LockOnTarget;
    public MovementBehavior PerformProjectileBehavior;

    [SerializeField]
    public float FlightSpeed;

    float Lifetime;

    [SerializeField]
    bool PlayerIsLockedOn;

    void Awake()
    {
        SetUpLockOnBehavior();
        FlightSpeed = 60f;
        Lifetime = 10f;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (PerformProjectileBehavior != null)
            PerformProjectileBehavior();   

        DegradeProjectileLife();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other != null && (!other.GetComponent<Projectile>() || (other.GetComponent<Projectile>() && other.GetComponent<Projectile>().PlayerOrigin != PlayerOrigin)))
        {
            if (other.transform.GetComponent<DamageableObject>())
                ApplyDamageToCorrectObject(other.transform);
            else
                ApplyDamageToCorrectObject(other.transform.root);
        }
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.collider != null && (!collision.collider.GetComponent<Projectile>() || (collision.collider.GetComponent<Projectile>() && collision.collider.GetComponent<Projectile>().PlayerOrigin != PlayerOrigin)))
    //    {
    //        if (collision.transform.GetComponent<DamageableObject>())
    //            ApplyDamageToCorrectObject(collision.transform);
    //        else
    //            ApplyDamageToCorrectObject(collision.transform.root);
    //    }
    //}

    private void ApplyDamageToCorrectObject(Transform o)
    {
        o.GetComponent<DamageableObject>().ReceiveDamage(2);

        GameObject particle;

        if (o.GetComponent<DamageableObject>().DamageSurfaceType == SurfaceType.ARMOR)
        {
            particle = Resources.Load("Prefabs/Testing/Armor Explosion Test") as GameObject;
            Instantiate(particle, o.position, Quaternion.identity);
        }      
        else
        {
            particle = Resources.Load("Prefabs/Testing/Body Explosion Test") as GameObject;
            Instantiate(particle, o.position, Quaternion.identity);
        }
            

        Destroy(gameObject);
    }

    #region commented out hit detection
    //private void CheckForProjectileHit(Collider h)
    //{
    //    if (h.transform.root.GetComponent<DamageableObject>() != null)
    //        ApplyDamageToCorrectObject(h);
    //    else
    //        Debug.Log(PlayerOrigin);
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other != null && other.transform.root != PlayerOrigin && (ArmorPiercingInteraction)ArmorInteractionValue != ArmorPiercingInteraction.PIERCING && !other.transform.IsChildOf(PlayerOrigin))
    //    {
    //        Debug.Log("object hit" + other.transform.root.name);
    //        CheckForProjectileHit(other);

    //        Destroy(gameObject);
    //    }
    //}

    //void ApplyDamageToCorrectObject(Collider colliderToDamage)
    //{
    //    Debug.Log("Damage applied");
    //    int damageDealt = 0;

    //    Debug.Log(colliderToDamage.transform.root.name);

    //    switch ((ArmorPiercingInteraction)ArmorInteractionValue)
    //    {
    //        case ArmorPiercingInteraction.DAMAGING:
    //            damageDealt = 2;
    //            break;
    //        //case ArmorPiercingInteraction.BREAKING:
    //        //    if (colliderToDamage.GetComponent<LiveEntity>() != null)
    //        //        damageDealt = colliderToDamage.GetComponent<LiveEntity>().Health;
    //        //    else if (colliderToDamage.GetComponent<ArmorPiece>() != null)
    //        //        damageDealt = colliderToDamage.GetComponent<ArmorPiece>().StructuralIntegrity;
    //        //    break;
    //    }

    //    //colliderToDamage.transform.root.GetComponent<DamageableObject>().ReceiveDamage(damageDealt);
    //}
    #endregion

    void DegradeProjectileLife()
    {
        Lifetime -= Time.fixedDeltaTime * 1.5f;

        if (Lifetime <= 0) Destroy(gameObject);
    }

    void SetUpLockOnBehavior()
    {
        PlayerIsLockedOn = LockOnTarget != null ? true : false;

        if (PlayerIsLockedOn)
        {
            switch ((LockOnHardness)LockOnHardnessValue)
            {
                case LockOnHardness.SOFT:
                    transform.LookAt(LockOnTarget);
                    PerformProjectileBehavior += ProjectileBehavior_SoftLockedMovement;
                    break;
                case LockOnHardness.HARD:
                    PerformProjectileBehavior += ProjectileBehavior_HardLockedMovement;
                    break;
            }
        }
        else
            PerformProjectileBehavior += ProjectileBehavior_SoftLockedMovement;
    }

    void ProjectileBehavior_HardLockedMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, LockOnTarget.position, (FlightSpeed * 1.5f) * Time.fixedDeltaTime);
    }

    void ProjectileBehavior_SoftLockedMovement()
    {
        transform.position += transform.forward * FlightSpeed * 1.5f * Time.fixedDeltaTime;
    }
}
