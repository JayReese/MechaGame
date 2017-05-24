using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Player : LiveEntity
{
    public int PlayerID;
    public int Health;
    public float MaxFuel, CurrentFuel;

    // Checks periodically for sufficient thiccness. 
    // Shut up Lex, it's staying and I don't care what you say. Fight me.
    public bool IsExtraThicc;
    [SerializeField]
    bool _playerPreGameRadarHasFinished;

    public PlayerState CurrentPlayerState;
    public LockOnState CurrentLockOnState;

    public byte WeaponLockHardnessValue;

    public List<Transform> TargetsInRange;
    Radar PlayerRadar;

    [SerializeField]
    public CommandExecution ExecuteCommand;

    // Use this for initialization
    protected void Start()
    {
        MaxFuel = 3;
        CurrentFuel = MaxFuel;
        CurrentPlayerState = PlayerState.ON_GROUND;
        PlayerRadar = transform.FindChild("Radar").GetComponent<Radar>();

        PlayerRadar.BeginDefaults(TargetsInRange);

        Health = 10;

        _playerPreGameRadarHasFinished = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!_playerPreGameRadarHasFinished)
        {
            TargetsInRange.Clear();
            _playerPreGameRadarHasFinished = true;
        }

        CheckForStateBasedFunctions();
    }

    protected void FixedUpdate()
    {
        PerformCommandExecution();
        CheckIfUsingSubweapons();
    }

    private void PerformCommandExecution()
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
        if (multiplier == 1 && CurrentFuel > 0 || multiplier == -1 && CurrentFuel < MaxFuel)
            CurrentFuel -= Time.deltaTime * 1.5f * multiplier;


        CurrentFuel = CurrentPlayerState == PlayerState.ON_GROUND && CurrentFuel > MaxFuel ? MaxFuel : CurrentFuel;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Enemy" || c.tag == "Controllable")
            AddTargetToRadarList(c.transform);
    }

    void AddTargetToRadarList(Transform target)
    {
        bool enemyCurrentlyListed = false;

        if (TargetsInRange.Count > 0)
        {
            for (byte i = 0; i < TargetsInRange.Count; i++)
            {
                if (TargetsInRange[i] == target)
                {
                    enemyCurrentlyListed = true;
                    break;
                }
            }
        }

        if (!enemyCurrentlyListed) TargetsInRange.Add(target);

        #region Currently commented out - Dictionary method for populating the radar list.
        //if (TargetsInRange.ContainsKey(target))
        //    enemyCurrentlyListed = true;

        //if (!enemyCurrentlyListed) TargetsInRange.Add(target, Vector3.Distance(target.position, gameObject.transform.position));
        #endregion
    }

    public void ActivateRadar(ref Transform lockOnTarget)
    {
        PlayerRadar.GetComponent<Radar>().Activate(TargetsInRange, ref lockOnTarget);
        transform.FindChild("Camera").GetComponent<CameraMovement>().CameraLockOn(lockOnTarget);
    }

    public void DeactivateLockOn()
    {
        GetComponentInChildren<CameraMovement>().RemoveLockOnTarget();
        PlayerRadar.GetComponent<Radar>().ClearEnemyList(TargetsInRange);
    }

    public void TakeDamage(int damageDealt)
    {
        Health -= damageDealt;
    }

    void CheckIfUsingSubweapons()
    {
        if (GetComponent<PlayerInput>().FirstSubweaponButtonPressed) ExecuteCommand += UseFirstSubweapon;
        if (GetComponent<PlayerInput>().SecondSubweaponButtonPressed) ExecuteCommand += UseSecondSubweapon;
    }

    void UseFirstSubweapon()
    {
        Debug.Log("First Subweapon used.");
    }

    void UseSecondSubweapon()
    {
        Debug.Log("Second subweapon used.");
    }
}