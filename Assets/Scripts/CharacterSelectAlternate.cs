using UnityEngine;
using System.Collections;
using System;

<<<<<<< HEAD:Assets/Scripts/PlayerInput.cs
public class PlayerInput : MonoBehaviour
{
    public float HorizontalMouseMovement { get; private set; }
    public float VerticalMouseMovement { get; private set; }
    public float HorizontalMovement { get; private set; }   
    public float VerticalMovement { get; private set; }

    public bool FirstSubweaponButtonPressed { get; private set; }
    public bool SecondSubweaponButtonPressed { get; private set; }

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
        BindActionInputs();
    }

    private void BindActionInputs()
    {
#if UNITY_EDITOR
        FirstSubweaponButtonPressed = Input.GetKeyDown(KeyCode.Q);
        SecondSubweaponButtonPressed = Input.GetKeyDown(KeyCode.E);
#endif   
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
=======
public class CharacterSelectAlternate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
>>>>>>> system:Assets/Scripts/CharacterSelectAlternate.cs
}
