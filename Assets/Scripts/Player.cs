using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : LiveEntity
{
    public int PlayerID;
    public float MaxFuel, CurrentFuel;

    // Checks periodically for sufficient thiccness. 
    // Shut up Lex, it's staying and I don't care what you say. Fight me.
    public bool IsExtraThicc;

    public PlayerState CurrentPlayerState;
    public LockOnState CurrentLockOnState;

    public List<Transform> TargetsInRange;
    Radar PlayerRadar;

    [SerializeField]
    public CommandExecution ExecuteCommand;

    // Use this for initialization
    void Start ()
    {
        MaxFuel = 3;
        CurrentFuel = MaxFuel;
        CurrentPlayerState = PlayerState.ON_GROUND;
        PlayerRadar = transform.FindChild("Radar").GetComponent<Radar>();

        PlayerRadar.BeginDefaults(TargetsInRange);

        //TargetsInRange = new Dictionary<Transform, float>();
        //TargetsInRange = new List<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckForStateBasedFunctions();
        PerformCommandExecution();
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
}