using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    public Transform PlayerCamera;
    Player PlayerReference;
    PlayerInput PInput;
    Weapon PlayerWeapon;
    [SerializeField] Rigidbody PlayerRigidbody;

    [SerializeField]
    float MovementSpeed, TurnSensitivity;

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
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void FixedUpdate()
    {
        RotateCharacter();
        Move();
        Hop();
    }

    void RotateCharacter()
    {
        transform.Rotate(new Vector3(transform.eulerAngles.x, PInput.HorizontalMouseMovement * (Time.deltaTime * TurnSensitivity) * 20f, transform.eulerAngles.z));
    }

    private void Move()
    {
        /// Gravity-based programming. If you want it, it's right here.
        //PlayerRigidbody.AddRelativeForce(PlayerCamera.transform.forward * 10f * VerticalMovement, ForceMode.Acceleration);
        //PlayerRigidbody.AddRelativeForce(Vector3.right * 10f * HorizontalMovement, ForceMode.Acceleration);

        // The reason trans pos is used to update the position rather than use Rigidbody addforce is because of rotational errors.
        // Transform.position takes in explicit rotation values, so there isn't any kind of implicit guessing on Unity's
        // part for where you've turned - it takes whatever you do quite literally. We can simulate acceleration through
        // simply using GetAxis, as well.

        transform.position += transform.forward * (MovementSpeed * GetModifiedSpeed()) * Time.fixedDeltaTime * PInput.VerticalMovement;
        transform.position += transform.right * (MovementSpeed * GetModifiedSpeed()) * Time.fixedDeltaTime * PInput.HorizontalMovement;

        // Stops forward velocity immediately when the directional buttons aren't being pressed.
        //if ((PInput.VerticalMovement == 0 && PInput.HorizontalMovement == 0))
        //    PlayerRigidbody.velocity = new Vector3(0, PlayerRigidbody.velocity.y, 0); 

        LockOn();
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
        PlayerReference.PState = (PlayerState)Input.GetAxisRaw("Jump");

        if (PlayerReference.CurrentFuel > 0 && Input.GetKey(PInput.Boosting))
            PlayerRigidbody.AddForce(transform.up * 20f, ForceMode.Acceleration);
    }

    private void LockOn()
    {
        PlayerReference.LOState = PInput.LockedOn ? LockOnState.LOCKED : LockOnState.FREE;

        if(PInput.LockedOn)
            transform.LookAt(GameObject.FindGameObjectWithTag("Enemy").transform.position);
    }
}
