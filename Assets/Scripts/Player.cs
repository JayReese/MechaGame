using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : LiveEntity
{
    public int PlayerID;
    public float MaxFuel, CurrentFuel;

    public PlayerState CurrentPlayerState;
    public LockOnState CurrentLockOnState;

    public  

	// Use this for initialization
	void Start ()
    {
        MaxFuel = 3;
        CurrentFuel = MaxFuel;
        CurrentPlayerState = PlayerState.ON_GROUND;
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
            Debug.Log(c.name);
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Enemy" || c.tag == "Controllable")
            Debug.Log("Left the thing.");
    }
}