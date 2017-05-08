using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    public Transform PlayerCamera;
    Player PlayerReference;
    PlayerInput PInput;
    Weapon PlayerWeapon;
    Rigidbody PlayerRigidbody;

    [SerializeField]
    float MovementSpeed, TurnSensitivity,
          LockOnStateSwitchCounter;

    // Use this for initialization
    void Start ()
    {
        PlayerCamera = transform.FindChild("Camera");
        PlayerRigidbody = GetComponent<Rigidbody>();

        PlayerReference = GetComponent<Player>();
        PInput = GetComponent<PlayerInput>();
        PlayerWeapon = transform.FindChild("Weapon").GetComponent<Weapon>();

        MovementSpeed = 10f;
        TurnSensitivity = 5f;

        LockOnStateSwitchCounter = 1;

        PInput.LockOnToggled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }

    void FixedUpdate()
    {
        RotateCharacter();
        
        Hop();
    }

    void RotateCharacter()
    {
        transform.Rotate(new Vector3(transform.eulerAngles.x, PInput.HorizontalMouseMovement * (Time.deltaTime * TurnSensitivity) * 20f, transform.eulerAngles.z));
    }

    void PrintFive()
    {
        Debug.Log(5);
    }

    void PrintSix()
    {
        Debug.Log(6);
    }

    private void Move()
    {
        #region Commented out - gravity-based programming. If you want it, it's right here.
        //PlayerRigidbody.AddRelativeForce(PlayerCamera.transform.forward * 10f * VerticalMovement, ForceMode.Acceleration);
        //PlayerRigidbody.AddRelativeForce(Vector3.right * 10f * HorizontalMovement, ForceMode.Acceleration);
        #endregion

        // The reason trans pos is used to update the position rather than use Rigidbody addforce is because of rotational errors.
        // Transform.position takes in explicit rotation values, so there isn't any kind of implicit guessing on Unity's
        // part for where you've turned - it takes whatever you do quite literally. We can simulate acceleration through
        // simply using GetAxis, as well.

        transform.position += transform.forward * (MovementSpeed * GetModifiedSpeed()) * Time.fixedDeltaTime * PInput.VerticalMovement;
        transform.position += transform.right * (MovementSpeed * GetModifiedSpeed()) * Time.fixedDeltaTime * PInput.HorizontalMovement;

        #region Commented out - velocity-based movement.
        // Stops forward velocity immediately when the directional buttons aren't being pressed.
        //if ((PInput.VerticalMovement == 0 && PInput.HorizontalMovement == 0))
        //    PlayerRigidbody.velocity = new Vector3(0, PlayerRigidbody.velocity.y, 0); 
        #endregion

        CheckLockOnState();
    }

    private float GetModifiedSpeed()
    {
        float mod = 1;

        if (PlayerWeapon.IsFiring)
            mod *= 0.3f;

        return mod;
    }

    private void Hop()
    {
        PlayerReference.CurrentPlayerState = (PlayerState)Input.GetAxisRaw("Jump");

        if (PlayerReference.CurrentFuel > 0 && Input.GetKey(PInput.Boosting))
            PlayerRigidbody.AddForce(transform.up * 20f, ForceMode.Acceleration);
    }

    private void CheckLockOnState()
    {

        #region Commented out - a switch statement for checking lock on.
        //if(Input.GetKeyDown(KeyCode.Q) && LockOnStateSwitchCounter <= 0)
        //{
        //    switch(PlayerReference.CurrentLockOnState)
        //    {
        //        case LockOnState.FREE:
        //            PlayerReference.ExecuteCommand += EngageLockOn;
        //            break;
        //        case LockOnState.LOCKED:
        //             BreakLockOn();
        //            break;
        //    }
        //}
        #endregion

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1))
        {
            PInput.LockOnToggled = !PInput.LockOnToggled;

            if (PInput.LockOnToggled) PlayerReference.ExecuteCommand = EngageLockOn;
            else PlayerReference.ExecuteCommand = BreakLockOn;
        }
#endif

        #region Commented out - if statements to determine if you can switch lock on states.
        //if (Input.GetKeyDown(KeyCode.Q) && PlayerReference.CurrentLockOnState == LockOnState.FREE)
        //{
        //    PlayerReference.CurrentLockOnState = LockOnState.LOCKED;
        //    EngageLockOn();
        //}

        //if (Input.GetKeyDown(KeyCode.Q) && PlayerReference.CurrentLockOnState == LockOnState.LOCKED)
        //{
        //    BreakLockOn();
        //}
        #endregion

    }

    private void EngageLockOn()
    {
        Debug.Log("Activating lock on");
        PlayerReference.CurrentLockOnState = LockOnState.LOCKED;
        PlayerReference.ActivateRadar(ref PlayerWeapon.LockOnTarget);
    }

    private void BreakLockOn()
    {
        Debug.Log("Break lock on");
        PlayerReference.CurrentLockOnState = LockOnState.FREE;
    }
}