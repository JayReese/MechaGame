using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : DamageableObject
{
    public int PlayerID;

    public int TeamNumber;
    //public Vector3 SpawnPosition { get; private set; }

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
    public BoostState CurrentPlayerBoostingState;
    public LockOnState CurrentLockOnState;
    public InterfacingState CurrentInterfacingState;

    public bool IsOnGround, IsCurrentlyControllable,
                CanUseSubweapons;

    [SerializeField]
    protected float FirstSubWeaponCooldownTimer, SecondSubWeaponCooldownTimer;
    [SerializeField]
    protected bool FirstSubweaponFinished, SecondSubweaponFinished;

    [SerializeField]
    //protected List<GameObject> OperationalArmorPieces;
    protected Transform ArmorPiecesReference, UniquePartsReference, BodyPartsReference;
    #endregion

    #region Reference Fields
    [HideInInspector] public Radar PRadar;
    [SerializeField] Weapon PlayerWeapon;
    [SerializeField] Transform PlayerCamera, PlayerDeathCamera;
    AudioSource PlayerAudioSource;
    #endregion

    #region Testing fields.

    bool isActivePlayer, colorSet;

    
    #endregion

    protected void Awake()
    {
        MaxFuel = 20;
        CurrentFuel = MaxFuel;
        colorSet = false;

        CanUseSubweapons = true;
        IsPersistingObject = true;
        IsPlayer = true;

        DamageSurfaceType = SurfaceType.PLAYER;
        CurrentInterfacingState = InterfacingState.SPECTATING;

        PRadar = transform.GetComponentInChildren<Radar>();
        PlayerWeapon = GetComponentInChildren<Weapon>();

        PlayerAudioSource = GetComponent<AudioSource>();

        //sOperationalArmorPieces = new List<GameObject>();

        PlayerCamera = transform.FindGrandchild("Camera");
        PlayerDeathCamera = transform.FindGrandchild("Death Camera");
        
        //Debug.Log(Enum.IsDefined(typeof(PoiseState), 2));
    }

    // Use this for initialization
    protected void Start()
    {
        //PRadar.BeginDefaults(TargetsInRange);
        IsTargetable = true;

        //Globals.PlaySoundClip(PlayerAudioSource, 0, 0);

        GetAllFunctionalPieces();
        TetherBodyPartsToParent();
    }

    private void TetherBodyPartsToParent()
    {
        BodyPartsReference = transform.FindGrandchild("Body");

        for (byte i = 0; i < BodyPartsReference.childCount; i++)
            BodyPartsReference.GetChild(i).GetComponent<BodyPart>().TetheredParentObject = gameObject.transform;

        transform.FindGrandchild("Weapon").GetComponent<Weapon>().TetheredPlayer = gameObject.transform;
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

        CurrentPlayerBoostingState = IsOnGround ? BoostState.ON_GROUND : CurrentPlayerBoostingState;

        Kill();
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
        if (CurrentPlayerBoostingState == BoostState.BOOSTING) ChangeFuel(1);
        else ChangeFuel(-1);
    }

    void ChangeFuel(float multiplier)
    {
        if(!GodModeActive)
        {
            if (multiplier == 1 && CurrentFuel > 0 || multiplier == -1 && CurrentFuel < MaxFuel)
                CurrentFuel -= Time.deltaTime * 1.5f * multiplier;
        }

        CurrentFuel = CurrentPlayerBoostingState == BoostState.ON_GROUND && CurrentFuel > MaxFuel ? MaxFuel : CurrentFuel;
    }

    public void UseWeapon()
    {
        PlayerWeapon.PerformWeaponOperations(PRadar.CurrentLockOnTarget);
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

    internal void ChangeBodyColor(Color teamColor)
    {
        Transform bodyparts = transform.FindGrandchild("Body");

        for (byte i = 0; i < bodyparts.childCount; i++)
            bodyparts.GetChild(i).GetComponent<MeshRenderer>().material.color = teamColor;
    }

    public void TogglePlayerResetting()
    {
        Debug.Log("Camera is recentering");
        
        GetComponentInChildren<PlayerMovement>().ReorientPlayerViaReset();
        GetComponentInChildren<CameraMovement>().ReorientToCenter();
        PRadar.ForceLockOnDisable();
    }   

    #region Lock On Mechanics
    public void ToggleRadar()
    {
        PRadar.PingRadar(CurrentLockOnState);
        AssignCorrectLockOnAction();
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
    #endregion

    private void ActivateGodMode()
    {
        if (Input.GetKeyDown(KeyCode.G)) GodModeActive = !GodModeActive;
    }

    #region Collision enter things. Might not want it.
    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag != "Controllable")
    //        IsOnGround = true;
    //}

    //public void OnCollisionStay(Collision collision)
    //{
    //    if (collision.collider.tag != "Controllable")
    //        IsOnGround = true;
    //}

    //public void OnCollisionExit(Collision collision)
    //{
    //    IsOnGround = false;
    //}
    #endregion
   
    // The player's team number is set up here.
    public void SetTeamNumber(int num)
    {
        TeamNumber = num;
    }

    protected override void OnEnable()
    {
        //transform.position = SpawnPosition;
    }

    public override void Kill(string g = "regular death")
    {

        base.Kill(g);

        bool isDead = Health <= 0;

        if (isDead)
            BodyPartsReference.gameObject.SetActive(false);

        if (RespawnTimer > 0)
        {
            transform.FindGrandchild("Camera").gameObject.SetActive(isDead);
            transform.FindGrandchild("Death Camera").gameObject.SetActive(!isDead);
        }
    }

    public void TestReload() { PlayerWeapon.Test_Reload(); }
}