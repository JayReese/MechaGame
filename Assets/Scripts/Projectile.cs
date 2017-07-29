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
    public float ProjectileLockOnWindow;

    [SerializeField]
    protected bool _projectileOrientWindowHasPassed,
         _projectileHasPassedTarget, _initialProjectileThresholdIsNegative;
    [SerializeField]
    float _currentLockOnWindow;

    [SerializeField]
    public float FlightSpeed;

    [SerializeField] float Lifetime;

    [SerializeField] bool PlayerIsLockedOn;

#if UNITY_EDITOR
    public WeaponDebugging wdb;
#endif

    protected void Awake()
    {
        Debug.Log("shoot");

        LockOnHardnessValue = 8;
        Lifetime = 10f;
        _currentLockOnWindow = ProjectileLockOnWindow;
    }

    // Use this for initialization
    protected void Start()
    {
        transform.LookAt(LockOnTarget);

        _projectileHasPassedTarget = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        _projectileOrientWindowHasPassed = _currentLockOnWindow <= 0;

        if (_currentLockOnWindow > 0)
            _currentLockOnWindow -= Time.deltaTime;
    }

    private bool CurrentPositionIsGreaterThanTarget()
    {
        float zForwardPos = LockOnTarget.position.z - transform.position.z;

        if (_initialProjectileThresholdIsNegative)
            if (zForwardPos > 0) return true;
        else
            if (zForwardPos < 0) return true;

        return false;
    }

    protected void FixedUpdate()
    {
        _projectileHasPassedTarget = CurrentPositionIsGreaterThanTarget();

        ReorientProjectileRotation();

        if (ProjectileLockOnWindow != 0f)
        {
            DegradeProjectileLife();
            GetComponent<Rigidbody>().AddForce(transform.forward * FlightSpeed, ForceMode.Impulse);
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, LockOnTarget.position, FlightSpeed / 2);
    }

    void ReorientProjectileRotation()
    {
        if ((ProjectileLockOnWindow != 0 && !_projectileOrientWindowHasPassed && !_projectileHasPassedTarget) || ProjectileLockOnWindow == 0)
            transform.LookAt(LockOnTarget.position);
    }

    protected void OnEnable()
    {
        _initialProjectileThresholdIsNegative = LockOnTarget.position.z - transform.position.z < 0;
    }

    //void OnDisable()
    //{
    //    Debug.Log("Bullet deactivated.");
    //    //transform.parent = WeaponOrigin.FindChild("Ammo Feeder");
    //}

    protected virtual void CheckForHit()
    {
        
    }

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
    
    /// <summary>
    /// Degrades the projectile's life over time.
    /// </summary>
    void DegradeProjectileLife()
    {
        Lifetime -= Time.fixedDeltaTime * 1f;

        if (Lifetime <= 0)
        {
            Destroy(gameObject);
            ReportResult(false);
        }
    }

#if UNITY_EDITOR
    void ReportResult(bool hit)
    {
        wdb.LogResult(hit);
    }
#endif
}
