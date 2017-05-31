using UnityEngine;
using System.Collections;
using System;

public class PlayerInputScheme
{
    [SerializeField]
    bool UsingControllers;

    public float HorizontalLook { get; private set; }
    public float VerticalLook { get; private set; }

    public float HorizontalMovement { get; private set; }
    public float VerticalMovement { get; private set; }
    public float BoostingThreshold { get; private set; }

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

#if UNITY_EDITOR
    public KeyCode Boosting { get; private set; }
#endif

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

        HorizontalMovement = Input.GetAxis("Horizontal");
        VerticalMovement = Input.GetAxis("Vertical");

        HorizontalLook = Input.GetAxis("Mouse X");

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
        HorizontalMovement = Input.GetAxisRaw("Joy0X");
        VerticalMovement = -Input.GetAxisRaw("Joy0Y");

        FirstSubweaponButtonPressed = Input.GetButtonDown("Alt1Fire0");
        SecondSubweaponButtonPressed = Input.GetButtonDown("Alt2Fire0");

        HorizontalLook = Input.GetAxisRaw("AltJoy0X");

        //IsBoosting = Input.GetButtonDown("JumpController0");

        MeleeUsed = Input.GetButtonDown("AltMelee0");
        MeleeInputThreshold = Input.GetAxisRaw("Melee0");

        // Firing inputs.
        TriggerPulled = Input.GetButtonDown("FireController0");
        TriggerPulledThreshold = Input.GetAxisRaw("AltFireController0");


    }
}