using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    public Transform PlayerCamera { get; private set; }

    Rigidbody PlayerRigidbody;
    Player PlayerRef;

    Vector3 _currentLockOnTargetPosition;

    [SerializeField]
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

        PlayerRef.MovementSpeed = 25f;
        PlayerRef.MoveSpeedModifier = 1.0f;

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

    public void ReorientPlayerViaReset()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void Move(float lookAxis, float movementAxis, float boostingThreshold, bool isBoosting, int ID)
    {
        #region Commented out - gravity-based programming. If you want it, it's right here.
        //PlayerRigidbody.AddRelativeForce(PlayerCamera.transform.forward * 10f * VerticalMovement, ForceMode.Acceleration);
        //PlayerRigidbody.AddRelativeForce(Vector3.right * 10f * HorizontalMovement, ForceMode.Acceleration);
        #endregion

        if (ID == PlayerRef.PlayerID)
        {
            //Debug.Log(ID + " is boosting: " + boostingThreshold);
    
            // The reason trans pos is used to update the position rather than use Rigidbody addforce is because of rotational errors.
            // Transform.position takes in explicit rotation values, so there isn't any kind of implicit guessing on Unity's
            // part for where you've turned - it takes whatever you do quite literally. We can simulate acceleration through
            // simply using GetAxis, as well.
            if (PlayerRef.CurrentLockOnState == LockOnState.FREE)
            {
                transform.position += transform.forward * (PlayerRef.MovementSpeed * PlayerRef.MoveSpeedModifier) * Time.fixedDeltaTime * movementAxis;
                transform.Rotate(0, lookAxis * Time.deltaTime * 150.0f, 0);
            }
            else
            {
                transform.position += transform.forward * (PlayerRef.MovementSpeed * PlayerRef.MoveSpeedModifier) * Time.fixedDeltaTime * movementAxis;
                transform.position += transform.right * (PlayerRef.MovementSpeed * PlayerRef.MoveSpeedModifier) * Time.fixedDeltaTime * lookAxis;
            }

            //transform.position += transform.right * PlayerRef.MovementSpeed * Time.fixedDeltaTime * movementXAxisDirection;

            #region Commented out - velocity-based movement.
            // Stops forward velocity immediately when the directional buttons aren't being pressed.
            //if ((PInput.VerticalMovement == 0 && PInput.HorizontalMovement == 0))
            //    PlayerRigidbody.velocity = new Vector3(0, PlayerRigidbody.velocity.y, 0); 
            #endregion

            //CorrectDodgeState(movementXAxisDirection, movementZAxisDirection);

            MaintainModelRotationToEnemy();

            
        }
    }

    void MaintainModelRotationToEnemy()
    {
        if (PlayerRef.ReturnLockOnTarget() != null)
        {
            //_currentLockOnTargetPosition = PlayerRef.ReturnLockOnTarget().position;
            //_currentLockOnTargetPosition.y = PlayerRef.IsOnGround ? 0.0f : _currentLockOnTargetPosition.y;
            PlayerModel.transform.LookAt(PlayerRef.ReturnLockOnTarget());
        }
        else
            MaintainModelRotationToWorld();
    }

    void MaintainModelRotationToWorld()
    {
        _currentLockOnTargetPosition = PlayerCamera.transform.forward;
        _currentLockOnTargetPosition.y = 0.0f;

        PlayerModel.transform.localEulerAngles = _currentLockOnTargetPosition;
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
    public void Boost(float boostingThresh, bool isBoosting)
    {
        int boosting = (boostingThresh != 0) || (isBoosting) ? 1 : 0;

        // This takes in if you're pressing the Jump button. It does a nifty thing of taking in the raw axis (either 1 or 0) and casts a
        // PlayerState into it. This changes the current PlayerState since you're either ON_GROUND or BOOSTING, with ON_GROUND being at
        // element 0.
        PlayerRef.CurrentPlayerState = (PlayerState)boosting;

        // This checks if you still have fuel and you're continuing to press the button.
        if (PlayerRef.CurrentFuel > 0 && PlayerRef.CurrentPlayerState == PlayerState.BOOSTING)
            PlayerRigidbody.AddForce(transform.up * PlayerRef.JumpJetStrength, ForceMode.Acceleration);
    }

    public void SetUpRagdollFeatures(bool isAlive)
    {
        if (isAlive)
            PlayerRigidbody.constraints = RigidbodyConstraints.None;
        else
            PlayerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
}