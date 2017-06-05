using UnityEngine;
using System.Collections;
using System;

public class PlayerInputScheme
{
    [SerializeField]
    bool UsingControllers;

    public float HorizontalLook { get; private set; }
    public float LookAxis { get; private set; }

    public float HorizontalMovement { get; private set; }
    public float MovementAxis { get; private set; }
    public float BoostingThreshold { get; private set; }
    public bool Boosting { get; private set; }

    #region Trigger inputs.
    // Trigger pulling variables.
    public bool TriggerPulled { get; private set; }
    public float TriggerPulledThreshold { get; private set; }
    #endregion

    #region Subweapon inputs.
    public bool FirstSubweaponButtonPressed { get; private set; }
    public bool SecondSubweaponButtonPressed { get; private set; }
    #endregion

    #region Melee inputs.
    public bool MeleeUsed { get; private set; }
    public float MeleeInputThreshold { get; private set; }
    #endregion

    #region Lock on inputs.
    public bool LockOnToggled { get; private set; }
    #endregion

    /// <summary>
    /// Loads the keyboard and controller control scheme setups.
    /// </summary>
    public void BindActionInputs()
    {
        LoadControllerControls();
        LoadKeyboardControls();
    }

    
    void LoadKeyboardControls()
    {
        FirstSubweaponButtonPressed = Input.GetKeyDown(KeyCode.Q);
        SecondSubweaponButtonPressed = Input.GetKeyDown(KeyCode.E);

        //HorizontalMovement = Input.GetAxis("Horizontal");
        MovementAxis = Input.GetAxis("Vertical");
        LookAxis = Input.GetAxis("Horizontal");

        LockOnToggled = Input.GetMouseButtonDown(1);

        TriggerPulled = Input.GetMouseButton(0); 

        MeleeUsed = Input.GetKeyDown(KeyCode.F);

        BoostingThreshold = Input.GetAxisRaw("Jump");
    }
    
    /// <summary>
    /// Loads the XBox 360 controller control scheme setup.
    /// </summary>
    void LoadControllerControls()
    {
        //Debug.Log("Controller");
        LookAxis = Input.GetAxisRaw("Joy0X");
        MovementAxis = -Input.GetAxisRaw("Joy0Y");

        FirstSubweaponButtonPressed = Input.GetButtonDown("Alt1Fire0");
        SecondSubweaponButtonPressed = Input.GetButtonDown("Alt2Fire0");

        HorizontalLook = Input.GetAxisRaw("AltJoy0X");

        BoostingThreshold = Input.GetAxisRaw("JumpController0");
        Boosting = Input.GetButton("JumpController0");

        //Debug.Log(Boosting);

        MeleeUsed = Input.GetButtonDown("AltMelee0");
        MeleeInputThreshold = Input.GetAxisRaw("Melee0");

        // Firing inputs.
        TriggerPulled = Input.GetButtonDown("FireController0");
        TriggerPulledThreshold = Input.GetAxisRaw("AltFireController0");
    }
}