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
    float MovementSpeed, TurnSensitivity;
    bool IsReorienting;

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

        PInput.LockOnToggled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void FixedUpdate()
    {
        RotateCharacter();
        Move();
        Boost();
    }

    void RotateCharacter()
    {
        if(PlayerReference.CurrentLockOnState != LockOnState.LOCKED)
            transform.Rotate(new Vector3(transform.eulerAngles.x, PInput.HorizontalMouseMovement * (Time.deltaTime * TurnSensitivity) * 20f, transform.eulerAngles.z));
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
        OrientToEnemy();
    }

    private void OrientToEnemy()
    {
        // This will only occur when the CurrentLockOnTarget is out of the view of the camera
        if (PlayerCamera.gameObject.GetComponent<CameraMovement>().LockOnTargetOutOfView)
        {
            #region Commented out - isn't smooth don't worry about it.
            // Here's where the (horizontal) magic happens - the player's camera is reoriented to look toward the enemy based on where they
            // are in relation to the camera.
            // NOTE: This still need a LOT of fixing - it needs to have more finesse and flexibility, and allow for more relativity like
            // changes in velocity.

            //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, (Vector3.Angle(PlayerCamera.GetComponent<CameraMovement>().CurrentLockOnTarget.position, transform.position) * 1.8f) * angleChange, 0), Time.deltaTime * 5f);
            //// Gets the x-axis position of the enemy in relation to the player's camera.
            //double frustumPositionX = Math.Round(PlayerCamera.GetComponent<Camera>().WorldToViewportPoint(PlayerCamera.GetComponent<CameraMovement>().CurrentLockOnTarget.transform.position).x, 1);

            //// Then, it evaluates the change in the angle. If frustumPositionX is more than zero, it's a positive change in angle,
            //// and if it's less than zero, it's a negative change in angle.
            //int angleChange = frustumPositionX > 0 ? 1 : -1;

            //Debug.Log("Angle change is " + angleChange); // a simple debug to check the angle change.

            //if (frustumPositionX > 0)
            //    Debug.Log("Enemy moved to right: " + frustumPositionX);
            //else
            //    Debug.Log("Enemy moved to left: " + frustumPositionX);
            #endregion

            transform.LookAt( new Vector3(PlayerCamera.GetComponent<CameraMovement>().CurrentLockOnTarget.transform.position.x, 0));
            //var lookPos = PlayerCamera.GetComponent<CameraMovement>().CurrentLockOnTarget.transform.position - transform.position;
            //lookPos.y = 0;
            //var rotation = Quaternion.LookRotation(lookPos * 3f);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4.0f);
            //transform.eulerAngles = new Vector3(0, PlayerCamera.GetComponent<CameraMovement>().CurrentLockOnTarget.transform.position.x, 0);
        }
            
    }

    // Not too sure if this actually works - I wanted to make it so that your speed was slower after you fired, but I'm not too sure we
    // need this anymore. CANDIDATE FOR DELETION.
    private float GetModifiedSpeed()
    {
        float mod = 1;

        if (PlayerWeapon.IsFiring)
            mod *= 0.3f;

        return mod;
    }

    /// <summary>
    /// Initiates flight.
    /// </summary>
    private void Boost()
    {
        // This takes in if you're pressing the Jump button. It does a nifty thing of taking in the raw axis (either 1 or 0) and casts a
        // PlayerState into it. This changes the current PlayerState since you're either ON_GROUND or BOOSTING, with ON_GROUND being at
        // element 0.
        PlayerReference.CurrentPlayerState = (PlayerState)Input.GetAxisRaw("Jump");

        // This checks if you still have fuel and you're continuing to press the button.
        if (PlayerReference.CurrentFuel > 0 && PlayerReference.CurrentPlayerState == PlayerState.BOOSTING)
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

    // Engages the lock on feature.
    private void EngageLockOn()
    {
        Debug.Log("Activating lock on");
        PlayerReference.CurrentLockOnState = LockOnState.LOCKED;
        PlayerReference.ActivateRadar(ref PlayerWeapon.LockOnTarget);
    }

    // Engages the lock on feature.
    private void BreakLockOn()
    {
        Debug.Log("Break lock on");
        PlayerReference.CurrentLockOnState = LockOnState.FREE;
    }
}