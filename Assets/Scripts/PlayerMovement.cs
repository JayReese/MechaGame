﻿using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    public Transform PlayerCamera { get; private set; }

    Rigidbody PlayerRigidbody;
    Player PlayerRef;

    GameObject PlayerModel;

    [SerializeField]
    float DodgeThresholdCounter;
    [SerializeField]
    bool CanDodge;

    // Use this for initialization
    void Start()
    {
        PlayerCamera = transform.FindChild("Camera");

        PlayerRef = GetComponent<Player>();
        PlayerRigidbody = GetComponent<Rigidbody>();

        PlayerModel = transform.FindChild("Model").gameObject;

        PlayerRef.MovementSpeed = 10f;
        PlayerRef.JumpJetStrength = 15f;

        DodgeThresholdCounter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }


    void RotateCharacter()
    {
        //if (PlayerReference.CurrentLockOnState != LockOnState.LOCKED)
            //transform.Rotate( new Vector3(transform.eulerAngles.x, PInput.HorizontalLook * (Time.deltaTime * TurnSensitivity) * 20f, transform.eulerAngles.z) );
    }

    public void Move(float movementXAxisDirection, float movementZAxisDirection, float boostingThreshold)
    {
        #region Commented out - gravity-based programming. If you want it, it's right here.
        //PlayerRigidbody.AddRelativeForce(PlayerCamera.transform.forward * 10f * VerticalMovement, ForceMode.Acceleration);
        //PlayerRigidbody.AddRelativeForce(Vector3.right * 10f * HorizontalMovement, ForceMode.Acceleration);
        #endregion

        // The reason trans pos is used to update the position rather than use Rigidbody addforce is because of rotational errors.
        // Transform.position takes in explicit rotation values, so there isn't any kind of implicit guessing on Unity's
        // part for where you've turned - it takes whatever you do quite literally. We can simulate acceleration through
        // simply using GetAxis, as well.

        transform.position += transform.forward * PlayerRef.MovementSpeed * Time.fixedDeltaTime * movementZAxisDirection;
        transform.position += transform.right * PlayerRef.MovementSpeed * Time.fixedDeltaTime * movementXAxisDirection;

        #region Commented out - velocity-based movement.
        // Stops forward velocity immediately when the directional buttons aren't being pressed.
        //if ((PInput.VerticalMovement == 0 && PInput.HorizontalMovement == 0))
        //    PlayerRigidbody.velocity = new Vector3(0, PlayerRigidbody.velocity.y, 0); 
        #endregion

        CorrectDodgeState(movementXAxisDirection, movementZAxisDirection);

        MaintainModelRotationToEnemy();
        Boost(boostingThreshold);
    }

    void MaintainModelRotationToEnemy()
    {
        if (PlayerRef.ReturnLockOnTarget() != null)
            PlayerModel.transform.LookAt(PlayerRef.ReturnLockOnTarget());
        else
            MaintainModelRotationToWorld();
    }
    
    void MaintainModelRotationToWorld()
    {
        PlayerModel.transform.localEulerAngles = PlayerCamera.transform.forward;
    }

    void CorrectDodgeState(float movementXAxis, float movementZAxis)
    {
        // Checks if the player has pushed the directional controls at least once, which means that they were able to dodge.
        if (movementXAxis != 0 || movementZAxis != 0)
        {
            if (DodgeThresholdCounter > 0) CanDodge = true;
            else CanDodge = false;
        }

        if (CanDodge) DodgeThresholdCounter -= Time.fixedDeltaTime * 0.5f;
    }




    /// <summary>
    /// Initiates flight.
    /// </summary>
    public void Boost(float boosting)
    {
        // This takes in if you're pressing the Jump button. It does a nifty thing of taking in the raw axis (either 1 or 0) and casts a
        // PlayerState into it. This changes the current PlayerState since you're either ON_GROUND or BOOSTING, with ON_GROUND being at
        // element 0.
        PlayerRef.CurrentPlayerState = (PlayerState)boosting;

        // This checks if you still have fuel and you're continuing to press the button.
        if (PlayerRef.CurrentFuel > 0 && PlayerRef.CurrentPlayerState == PlayerState.BOOSTING)
            PlayerRigidbody.AddForce(transform.up * (PlayerRef.JumpJetStrength * 3f), ForceMode.Acceleration);
    }
    
}