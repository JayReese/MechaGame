﻿using UnityEngine;
using System.Collections;
using System;

public class PlayerControls : MonoBehaviour
{
    Player PlayerReference;
    PlayerMovement Movement;
    PlayerInputScheme PlayerInput;

    // Use this for initialization
    void Start()
    {
        PlayerReference = GetComponent<Player>();
        Movement = GetComponent<PlayerMovement>();
        PlayerInput = GetComponent<PlayerInputScheme>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput.BindActionInputs();
            
        CheckActionControls();
    }

    private void CheckActionControls()
    {
        // This function continually runs without a gate because the DodgeState requires it.
        if(PlayerReference.PlayerID == PlayerInput._currentPlayerID)
        {
            Movement.Move(PlayerInput.LookAxis, PlayerInput.MovementAxis, PlayerInput.BoostingThreshold, PlayerInput.Boosting, PlayerReference.PlayerID);

            if (PlayerInput.TriggerPulled || PlayerInput.TriggerPulledThreshold != 0)
                PlayerReference.UseWeapon();

            if (PlayerInput.LockOnToggled)
                PlayerReference.ToggleRadar();
        }
            

        //Debug.Log(Input.GetAxis("JumpController0"));
    }
}
