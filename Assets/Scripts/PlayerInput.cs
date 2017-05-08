﻿using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    public float HorizontalMouseMovement { get; private set; }
    public float VerticalMouseMovement { get; private set; }
    public float HorizontalMovement { get; private set; }   
    public float VerticalMovement { get; private set; }

    [SerializeField] float SecondaryFire;
    public bool LockOnToggled;

#if UNITY_EDITOR
    public KeyCode Boosting { get; private set; }
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
        HorizontalMovement = Input.GetAxis("Horizontal");
        VerticalMovement = Input.GetAxis("Vertical");

        HorizontalMouseMovement = Input.GetAxis("Mouse X");

        Boosting = KeyCode.Space;
#endif
    }
}
