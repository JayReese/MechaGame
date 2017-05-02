using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : LiveEntity
{
    public int PlayerID;
    public float MaxFuel, CurrentFuel;

    public PlayerState CurrentPlayerState;
    public LockOnState CurrentLockOnState;

    //public List<Transform> TargetsInRange;
    public Dictionary<Transform, float> TargetsInRange;
    Radar PlayerRadar;

    [SerializeField]
    CommandExecution ExecuteCommand;

    // Use this for initialization
    void Start ()
    {
        MaxFuel = 3;
        CurrentFuel = MaxFuel;
        CurrentPlayerState = PlayerState.ON_GROUND;
        PlayerRadar = transform.FindChild("Radar").GetComponent<Radar>();
        TargetsInRange = new Dictionary<Transform, float>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckForStateBasedFunctions();
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

        #region Currently commented out - code for populating targets in range.
        //if(TargetsInRange.Count > 0)
        //{
        //    for (byte i = 0; i < TargetsInRange.Count; i++)
        //    {
        //        if (TargetsInRange[i] == target)
        //        {
        //            enemyCurrentlyListed = true;
        //            break;
        //        }
        //    }
        //}
        #endregion

        if (TargetsInRange.ContainsKey(target))
            enemyCurrentlyListed = true;
        
        if (!enemyCurrentlyListed) TargetsInRange.Add(target, Vector3.Distance(target.position, gameObject.transform.position));
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Enemy" || c.tag == "Controllable")
            Debug.Log("Left the thing.");
    }

    public void ActivateRadar()
    {
        PlayerRadar.Activate();
    }
}