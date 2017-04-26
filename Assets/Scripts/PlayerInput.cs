using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    public float HorizontalMouseMovement { get; private set; }
    public float VerticalMouseMovement { get; private set; }
    public float HorizontalMovement { get; private set; }
    public float VerticalMovement { get; private set; }

#if UNITY_EDITOR
    public KeyCode IsJumping { get; private set; }
#endif

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        BindMovement();

    }

    void BindMovement()
    {
#if UNITY_EDITOR 
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        VerticalMovement = Input.GetAxisRaw("Vertical");

        HorizontalMouseMovement = Input.GetAxisRaw("Mouse X");
        //IsJumping = 
#endif
    }
}
