using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Camera PlayerCamera;

    [SerializeField]
    float HorizontalMovement, VerticalMovement;

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        BindMovement();
        Move();
    }

    private void Move()
    {
        
    }

    void BindMovement()
    {
#if UNITY_EDITOR
        HorizontalMovement = Input.GetAxisRaw("PC Horizontal");
        VerticalMovement = Input.GetAxisRaw("PC Vertical");
#endif
    }
}
