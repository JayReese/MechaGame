using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    public int LockOnHardnessValue, BaseDamage;
    public ArmorPiercingInteraction ArmorInteractionValue;
    public Transform WeaponOrigin, PlayerOrigin, LockOnTarget;
    public MovementBehavior PerformProjectileBehavior;

    private Quaternion _rotation;
    private Vector3 _direction;
   

    [SerializeField]
    public float FlightSpeed;

    float Lifetime;

    [SerializeField]
    bool PlayerIsLockedOn;

#if UNITY_EDITOR
    public WeaponDebugging wdb;
#endif

    void Awake()
    {
        //SetUpLockOnBehavior();
        LockOnHardnessValue = 8;
        Lifetime = 10f;
    }

    // Use this for initialization
    void Start()
    {
        transform.LookAt(LockOnTarget);
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

        CheckForHit();

        ReorientProjectileRotation();
        //transform.position += transform.forward * FlightSpeed * 2f * Time.fixedDeltaTime;

        GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
        //Debug.Log(LockOnTarget.position.x / transform.position.x);
    }

    void ReorientProjectileRotation()
    {
        #region commented out, angle calculation method of determining reorientation.
        //Debug.Log(Vector3.Angle(transform.position, LockOnTarget.position));

        //if ((transform.position.x / LockOnTarget.position.x < 1 || transform.position.x / LockOnTarget.position.x > 1))
        //{
        //    var angleDegree = transform.position.x / LockOnTarget.position.x < 1 ? -1 : 1;
        //    var angle = Vector3.Angle(LockOnTarget.position - transform.position, transform.forward);

        //    if(LockOnTarget.position.z / transform.position.z > 0)
        //        transform.Rotate(0, angle * Time.fixedDeltaTime * angleDegree, 0);
        //}
        #endregion

        #region commented out, quaternion slerping of method of determining reorientation.
        //if (LockOnTarget.position.z / transform.position.z > 0.1f)
        //{
        //    _direction = (LockOnTarget.position - transform.position).normalized;
        //    _rotation = Quaternion.LookRotation(_direction);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.fixedDeltaTime * LockOnHardnessValue);
        //}
        #endregion

        if (LockOnTarget.position.z / transform.position.z > 2f)
        {
            if(LockOnTarget.position.x / transform.position.x >= 1f || LockOnTarget.position.x / transform.position.x <= -1f)
                transform.LookAt(LockOnTarget.position);
        }
    }
    //void OnEnable()
    //{
    //    transform.parent = null;
    //    transform.position = WeaponOrigin.FindChild("Weapon Emitter").transform.position;
    //    transform.rotation = WeaponOrigin.FindChild("Weapon Emitter").transform.rotation;
    //}

    //void OnDisable()
    //{
    //    Debug.Log("Bullet deactivated.");
    //    //transform.parent = WeaponOrigin.FindChild("Ammo Feeder");
    //}

    public void OnTriggerEnter(Collider other)
    {
        //if (other != null && (!other.GetComponent<Projectile>() || (other.GetComponent<Projectile>() && other.GetComponent<Projectile>().PlayerOrigin != PlayerOrigin)))
        //{
        //    //if (other.transform.GetComponent<DamageableObject>())
        //    //    ApplyDamageToCorrectObject(other.transform);
        //    //else
        //    //    ApplyDamageToCorrectObject(other.transform.FindGrandparent("Players"));


        //}

        //if (other.gameObject.layer != 11)
        //{
        //    if (other.tag == "Projectile" && other.GetComponent<Projectile>())
        //    {
        //         if(other.GetComponent<Projectile>().PlayerOrigin == PlayerOrigin)
        //            Debug.Log("Own projectile hit lolol");
        //    }
        //    else if ((other.GetComponent<ArmorPiece>()))
        //    {
        //        ApplyDamageToCorrectObject(other.transform);
        //    }
        //    else
        //    {
        //        ApplyDamageToCorrectObject(other.GetComponent<BodyPart>().TetheredParentObject);
        //        Debug.Log(other.GetComponent<BodyPart>().TetheredParentObject + " hit");
        //    }

        //}

        if (other.GetComponent<TargetDebugging>())
        {
            ReportResult(true);
            Debug.Log("Target hit");
        }  
        else
        {
            ReportResult(false);
            Debug.Log("Hit");
        }
            
        Destroy(gameObject);
    }

    void CheckForHit()
    {
        //if (Globals.RaycastHitTarget(transform.position, transform.forward, 3f).collider != null)
        //{
        //    Collider hit = Globals.RaycastHitTarget(transform.position, transform.forward, 3f).collider;

        //    if (hit.gameObject.layer != 11 && hit.transform != WeaponOrigin)
        //    {
        //        if (hit.tag == "Projectile" && hit.GetComponent<Projectile>())
        //        {
        //            if (hit.GetComponent<Projectile>().PlayerOrigin == PlayerOrigin)
        //                Debug.Log("Own projectile hit lolol");
        //        }
        //        else if ((hit.GetComponent<ArmorPiece>()))
        //        {
        //            //ApplyDamageToCorrectObject(hit.transform);
        //            Debug.Log("Armor piece hit");
        //        }
        //        else
        //        {
        //            //ApplyDamageToCorrectObject(hit.GetComponent<BodyPart>().TetheredParentObject);
        //            Debug.Log(hit.GetComponent<BodyPart>().TetheredParentObject + " hit");
        //        }

        //    }

        //    gameObject.SetActive(false);
        //}

        //if (Globals.RaycastHitTarget(transform.position, transform.forward, 3f).collider != null)
        //{
        //    Debug.Log("Hit");
        //}    
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
        Lifetime -= Time.fixedDeltaTime * 1f;

        if (Lifetime <= 0)
        {
            Destroy(gameObject);
            ReportResult(false);
        }
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

    void ReportResult(bool hit)
    {
        wdb.LogResult(hit);
    }
}
