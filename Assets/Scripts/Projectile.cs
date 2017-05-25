using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    public ArmorPiercingInteraction ArmorInteraction;
    public Transform WeaponOrigin, PlayerOrigin, LockOnTarget;
    public MovementBehavior PerformProjectileBehavior;

    [SerializeField]
    public float FlightSpeed;

    float Lifetime;

    [SerializeField]
    bool PlayerIsLockedOn;

    [SerializeField]
    int LockOnHardnessValue;


    void Awake()
    {
        SetUpLockOnBehavior();
        FlightSpeed = 60f;
        Lifetime = 10f;
    }

    // Use this for initialization
    void Start()
    {
        

        //PlayerIsLockedOn = LockOnTarget != null ? true : false;

        //if (!PlayerIsLockedOn)
        //    GetComponent<Rigidbody>().AddForce(transform.forward * FlightSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (PerformProjectileBehavior != null) PerformProjectileBehavior();

        DegradeProjectileLife();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != PlayerOrigin && !other.transform.IsChildOf(PlayerOrigin))
        {
            if (other.GetComponent<IDamageable>() != null)
            {
                if (other.GetComponent<ArmorPiece>())
                    other.GetComponent<ArmorPiece>().Degrade(2);
            }

            Destroy(gameObject);
            PerformProjectileBehavior = null;
        }
    }

    void ApplyDamageToCorrectObject()
    {
        switch (ArmorInteraction)
        {
            case ArmorPiercingInteraction.BLOCKED:

                break;
        }
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
