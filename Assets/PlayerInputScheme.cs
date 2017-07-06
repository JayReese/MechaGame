using UnityEngine;
using System.Collections;
using System;

public class PlayerInputScheme : MonoBehaviour
{
    [SerializeField]
    bool UsingControllers;
    public int _currentPlayerID; 
    public float HorizontalLook { get; private set; }
    public float LookAxis { get; private set; }

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

    #region testing
    public bool Test_Reloading { get; private set; }
    #endregion

    public bool IsRecentering { get; private set; }

    void Start()
    {
<<<<<<< HEAD:Assets/PlayerInputScheme.cs
        // IDs are currently set by grabbing the component of the player.
=======
>>>>>>> input-mechanics:Assets/Scripts/PlayerInputScheme.cs
        _currentPlayerID = GetComponent<Player>().PlayerID;
    }
    
    /// <summary>
    /// Loads the keyboard and controller control scheme setups.
    /// </summary>
    public void BindActionInputs()
    {
        //LoadControllerControls();
        LoadKeyboardControls();
    }

    void LoadKeyboardControls()
    {
        FirstSubweaponButtonPressed = Input.GetKey(KeyCode.Q);
        SecondSubweaponButtonPressed = Input.GetKey(KeyCode.E);

        MovementAxis = Input.GetAxis("Vertical");
        LookAxis = Input.GetAxis("Horizontal");

        LockOnToggled = Input.GetMouseButtonDown(1);

        TriggerPulled = Input.GetMouseButton(0); 

        MeleeUsed = Input.GetKeyDown(KeyCode.F);

        BoostingThreshold = Input.GetAxisRaw("Jump");

        IsRecentering = Input.GetKeyDown(KeyCode.R);
    }
    
    /// <summary>
    /// Loads the XBox 360 controller control scheme setup.
    /// </summary>
    void LoadControllerControls()
    {
        //Debug.Log("Controller");
        LookAxis = Input.GetAxisRaw("Joy" + _currentPlayerID + "X");
        MovementAxis = -Input.GetAxisRaw("Joy" + _currentPlayerID + "Y");

        FirstSubweaponButtonPressed = Input.GetButtonDown("Alt1Fire" + _currentPlayerID);
        SecondSubweaponButtonPressed = Input.GetButtonDown("Alt2Fire" + _currentPlayerID);

        HorizontalLook = Input.GetAxisRaw("AltJoy" + _currentPlayerID + "X");

        BoostingThreshold = Input.GetAxisRaw("JumpController" + _currentPlayerID);
        Boosting = Input.GetButton("JumpController" + _currentPlayerID);

        LockOnToggled = Input.GetButtonDown("Target" + _currentPlayerID);

        //Debug.Log("b " + Boosting);

        MeleeUsed = Input.GetButtonDown("AltMelee" + _currentPlayerID);
        MeleeInputThreshold = Input.GetAxisRaw("Melee" + _currentPlayerID);

        IsRecentering = Input.GetButtonDown("RecenterCamera" + _currentPlayerID);

        // Firing inputs.
        TriggerPulled = Input.GetButtonDown("FireController" + _currentPlayerID);
        //Debug.Log(_currentPlayerID + " pulled trigger? " + TriggerPulled);
        TriggerPulledThreshold = Input.GetAxisRaw("AltFireController" + _currentPlayerID);
        //Debug.Log(_currentPlayerID + " pulled trigger? " + TriggerPulledThreshold);\

        Test_Reloading = Input.GetButtonDown("TestReload" + _currentPlayerID);

    }
}