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
        CheckForProjectileHit();

        if (PerformProjectileBehavior != null)
            PerformProjectileBehavior();   

        DegradeProjectileLife();
    }

    private void CheckForProjectileHit()
    {
        RaycastHit h = Globals.RaycastHitTarget(transform.position, Vector3.forward, 2f);

        if (h.collider != null && (ArmorPiercingInteraction)ArmorInteractionValue != ArmorPiercingInteraction.PIERCING)
        {
            if (h.collider.gameObject.GetComponent<DamageableObject>() != null)
                ApplyDamageToCorrectObject(h.collider);
            else
                Debug.Log("No damage applied");

            Destroy(gameObject);
        }
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform != PlayerOrigin && !other.transform.IsChildOf(PlayerOrigin))
    //    {
    //        if (other.GetComponent<IDamageable>() != null)
    //            other.GetComponent<IDamageable>().ReceiveDamage(2);

    //        Destroy(gameObject);
            
    //    }
    //}

    void ApplyDamageToCorrectObject(Collider colliderToDamage)
    {
        int damageDealt = 0;

        switch ((ArmorPiercingInteraction)ArmorInteractionValue)
        {
            case ArmorPiercingInteraction.DAMAGING:
                damageDealt = 2;
                break;
            //case ArmorPiercingInteraction.BREAKING:
            //    if (colliderToDamage.GetComponent<LiveEntity>() != null)
            //        damageDealt = colliderToDamage.GetComponent<LiveEntity>().Health;
            //    else if (colliderToDamage.GetComponent<ArmorPiece>() != null)
            //        damageDealt = colliderToDamage.GetComponent<ArmorPiece>().StructuralIntegrity;
            //    break;
        }

        colliderToDamage.GetComponent<DamageableObject>().ReceiveDamage(damageDealt);
    }

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
        transform.GetComponent<Rigidbody>().AddForce(transform.forward * FlightSpeed / 9f, ForceMode.Impulse);
    }
}
