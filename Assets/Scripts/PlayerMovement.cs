using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Transform PlayerCamera;
    Player PlayerReference;
    PlayerInput PInput;
    [SerializeField] Rigidbody PlayerRigidbody;

    // Use this for initialization
    void Start ()
    {
        PlayerCamera = transform.FindChild("Camera");
        PlayerRigidbody = GetComponent<Rigidbody>();

        PlayerReference = GetComponent<Player>();
        PInput = GetComponent<PlayerInput>();
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
        transform.Rotate(new Vector3(transform.eulerAngles.x, PInput.HorizontalMouseMovement * (Time.deltaTime * 5f) * 20f, transform.eulerAngles.z));
    }

    private void Move()
    {
        /// Gravity-based programming. If you want it, it's right here.
        //PlayerRigidbody.AddRelativeForce(PlayerCamera.transform.forward * 10f * VerticalMovement, ForceMode.Acceleration);
        //PlayerRigidbody.AddRelativeForce(Vector3.right * 10f * HorizontalMovement, ForceMode.Acceleration);

        transform.position += transform.forward * 10f * Time.fixedDeltaTime * PInput.VerticalMovement;
        transform.position += Vector3.right * 10f * Time.fixedDeltaTime * PInput.HorizontalMovement;

        // Stops forward velocity immediately when the directional buttons aren't being pressed.
        if ((PInput.VerticalMovement == 0 && PInput.HorizontalMovement == 0))
            PlayerRigidbody.velocity = new Vector3(0, PlayerRigidbody.velocity.y, 0);
                
    }

    private void Hop()
    {
        if(PlayerReference.CurrentFuel > 0)
            PlayerRigidbody.AddForce(transform.up * 10f, ForceMode.Acceleration);

        PlayerReference.States = PlayerState.BOOSTING;
    }
}
