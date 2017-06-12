using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : DamageableObject
{
    public int PlayerID;

    // Checks periodically for sufficient thiccness. 
    // Shut up Lex, it's staying and I don't care what you say. Fight me.
    public bool IsExtraThicc;

    [SerializeField]
    bool GodModeActive;

    public byte WeaponLockHardnessValue;

    public List<Transform> TargetsInRange;

    public CommandExecution ExecuteCommand;

    #region Player Stats
    public float MovementSpeed, MoveSpeedModifier,
        JumpJetStrength, MaxFuel, CurrentFuel;

    [SerializeField]
    protected float FirstSubWeaponCooldown, SecondSubWeaponCooldown;

    #endregion

    #region Player statuses.
    public PlayerState CurrentPlayerState;
    public LockOnState CurrentLockOnState;

    public bool IsOnGround, IsCurrentlyControllable,
                CanUseSubweapons;

    [SerializeField]
    protected float FirstSubWeaponCooldownTimer, SecondSubWeaponCooldownTimer;
    [SerializeField]
    protected bool FirstSubweaponFinished, SecondSubweaponFinished;

    [SerializeField]
    //protected List<GameObject> OperationalArmorPieces;
    protected Transform ArmorPiecesReference, UniquePartsReference;
    #endregion

    #region Reference Fields
    [HideInInspector] public Radar PRadar;
    Weapon PlayerWeapon;
    Transform PlayerCamera;
    #endregion

    #region Testing fields.
    
    bool isActivePlayer;
    #endregion

    protected void Awake()
    {
        MaxFuel = 7;
        CurrentFuel = MaxFuel;

        CanUseSubweapons = true;
        IsPersistingObject = true;
        IsCurrentlyControllable = true;

        DamageSurfaceType = SurfaceType.PLAYER;

        PRadar = transform.GetComponentInChildren<Radar>();
        PlayerWeapon = GetComponentInChildren<Weapon>();

        //sOperationalArmorPieces = new List<GameObject>();

        PlayerCamera = transform.FindChild("Camera");
    }

    // Use this for initialization
    protected void Start()
    {
        //PRadar.BeginDefaults(TargetsInRange);
        IsTargetable = true;

        GetAllFunctionalPieces();
    }

    private void GetAllFunctionalPieces()
    {
        ArmorPiecesReference = transform.FindGrandchild("Armor Pieces");
        UniquePartsReference = transform.FindGrandchild("Unique Pieces");
    }

    // Update is called once per frame
    protected void Update()
    {       
        CheckForStateBasedFunctions();
        ActivateGodMode();

        CorrectLockOnEdgeCase();

        CurrentPlayerState = IsOnGround ? PlayerState.ON_GROUND : CurrentPlayerState;

        if (Health <= 0)
        {
            IsCurrentlyControllable = false;
            GetComponent<PlayerMovement>().SetUpRagdollFeatures(IsCurrentlyControllable);
            gameObject.SetActive(false);
        }

    }

    private void CorrectLockOnEdgeCase()
    {
        if (PRadar.CurrentLockOnTarget == null && CurrentLockOnState != LockOnState.FREE)
            CurrentLockOnState = LockOnState.FREE;
    }

    protected void CheckIfUsingMelee()
    {
        // Not implemented yet.
    }

    protected void PerformCommandExecution()
    {
        if (ExecuteCommand != null)
        {
            ExecuteCommand();
            ExecuteCommand = null;
        }
    }

    void CheckForStateBasedFunctions()
    {
        if (CurrentPlayerState == PlayerState.BOOSTING) ChangeFuel(1);
        else ChangeFuel(-1);
    }

    void ChangeFuel(float multiplier)
    {
        if(!GodModeActive)
        {
            if (multiplier == 1 && CurrentFuel > 0 || multiplier == -1 && CurrentFuel < MaxFuel)
                CurrentFuel -= Time.deltaTime * 1.5f * multiplier;
        }

        CurrentFuel = CurrentPlayerState == PlayerState.ON_GROUND && CurrentFuel > MaxFuel ? MaxFuel : CurrentFuel;
    }

    #region Commented out - old radar targeting system.
    //void OnTriggerEnter(Collider c)
    //{
    //    if (c.tag == "Enemy" || c.tag == "Controllable")
    //        AddTargetToRadarList(c.transform);
    //}

    //void AddTargetToRadarList(Transform target)
    //{
    //    bool enemyCurrentlyListed = false;

    //    if (TargetsInRange.Count > 0)
    //    {
    //        for (byte i = 0; i < TargetsInRange.Count; i++)
    //        {
    //            if (TargetsInRange[i] == target)
    //            {
    //                enemyCurrentlyListed = true;
    //                break;
    //            }
    //        }
    //    }

    //    if (!enemyCurrentlyListed) TargetsInRange.Add(target);

    //    #region Currently commented out - Dictionary method for populating the radar list.
    //    //if (TargetsInRange.ContainsKey(target))
    //    //    enemyCurrentlyListed = true;

    //    //if (!enemyCurrentlyListed) TargetsInRange.Add(target, Vector3.Distance(target.position, gameObject.transform.position));
    //    #endregion
    //}

    //public void ActivateRadar(ref Transform lockOnTarget)
    //{
    //    PlayerRadar.GetComponent<Radar>().Activate(TargetsInRange, ref lockOnTarget);
    //    transform.FindChild("Camera").GetComponent<CameraMovement>().CameraLockOn(lockOnTarget);
    //}

    //public void DeactivateLockOn()
    //{
    //    GetComponentInChildren<CameraMovement>().RemoveLockOnTarget();
    //    PlayerRadar.GetComponent<Radar>().ClearEnemyList(TargetsInRange);
    //}
    #endregion

    public void UseWeapon()
    {
        PlayerWeapon.PerformWeaponOperations(PRadar.CurrentLockOnTarget);
    }

    public void TakeDamage(int damageDealt)
    {
        Health -= damageDealt;
    }

    public void ActivateSubweapon(byte number)
    {
        switch(number)
        {
            case 1:
                ExecuteCommand += UseFirstSubweapon;
                break;
            case 2:
                ExecuteCommand += UseSecondSubweapon;
                break;
            default:
                throw new Exception("No subweapon known by that number.");
        }
    }

    #region Subweapon system.
    protected virtual void UseFirstSubweapon()
    {
        if (FirstSubWeaponCooldownTimer <= 0)
            FirstSubWeaponCooldownTimer = FirstSubWeaponCooldown;
        else Debug.Log("First subweapon can't be used.");
    }

    protected virtual void UseSecondSubweapon()
    {
        if (FirstSubWeaponCooldownTimer <= 0)
            SecondSubWeaponCooldownTimer = SecondSubWeaponCooldown;
        else Debug.Log("Second subweapon can't be used.");
    }

    protected void ManageCooldownTimers()
    {
        FirstSubWeaponCooldownTimer = FirstSubWeaponCooldownTimer <= 0 ? 0 : FirstSubWeaponCooldownTimer -= 1.5f * Time.fixedDeltaTime;
        SecondSubWeaponCooldownTimer = SecondSubWeaponCooldownTimer <= 0 ? 0 : SecondSubWeaponCooldownTimer -= 1.5f * Time.fixedDeltaTime;

        if (FirstSubWeaponCooldownTimer == 0) FirstSubweaponFinished = false;
        if (SecondSubWeaponCooldownTimer == 0) SecondSubweaponFinished = false; 
    }
    #endregion

    /// <summary>
    /// From the DamageableObject superclass, which allows an object to take damage. In this context, all LiveEntities (entities with some form of AI/controller) will be harmed by things that call this function.
    /// </summary>
    /// <param name="amount"></param>
    public override void ReceiveDamage(int amount)
    {
        base.ReceiveDamage(amount);
        //TakeDamage(amount);
        Debug.Log("damage dealt to body, " + Health + " HP left.");
    }

    public void TogglePlayerResetting()
    {
        Debug.Log("Camera is recentering");
            
        //transform.position = transform.position;
        GetComponentInChildren<PlayerMovement>().ReorientPlayerViaReset();
        GetComponentInChildren<CameraMovement>().ReorientToCenter();
        PRadar.ForceLockOnDisable();
    }   

    private void Kill()
    {
        Destroy(gameObject);
    }

    public void ToggleRadar()
    {
        AssignCorrectLockOnAction();

        PRadar.PingRadar(CurrentLockOnState);
    }

    private void AssignCorrectLockOnAction()
    {
        if (CurrentLockOnState == LockOnState.FREE) ExecuteCommand += EngageLockOn;
        else ExecuteCommand += BreakLockOn;
    }

    public Transform ReturnLockOnTarget()
    {
        return PRadar.CurrentLockOnTarget;
    }

    // Engages the lock on feature.
    private void EngageLockOn()
    {
        Debug.Log("Activating lock on");
        if(PRadar.CurrentLockOnTarget != null) CurrentLockOnState = LockOnState.LOCKED;
    }

    // Engages the lock on feature.
    private void BreakLockOn()
    {
        Debug.Log("Break lock on");
        CurrentLockOnState = LockOnState.FREE;
    }

    private void ActivateGodMode()
    {
        if (Input.GetKeyDown(KeyCode.G)) GodModeActive = !GodModeActive;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Controllable")
            IsOnGround = true;
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag != "Controllable")
            IsOnGround = true;
    }

    public void OnCollisionExit(Collision collision)
    {
        IsOnGround = false;
    }
}