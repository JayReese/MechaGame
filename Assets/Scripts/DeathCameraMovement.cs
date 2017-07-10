using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCameraMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed;

	// Use this for initialization
	void Start ()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
        _moveSpeed = 1f;
	}
	
    void FixedUpdate()
    {
        _moveSpeed = Input.GetKey(KeyCode.LeftShift) ? 5f : 1f;

        transform.position += transform.forward * Input.GetAxisRaw("Vertical") * _moveSpeed;
        transform.Rotate(0, Input.GetAxisRaw("Mouse X") * 5f, 0);
    }
}
