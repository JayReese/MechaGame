using UnityEngine;
using System.Collections;

public class ControllerDebugger : MonoBehaviour {
    
    public int ControllerCount;

	// Use this for initialization
	void Start ()
    {
#if UNITY_EDITOR
        Debug.Log("This is Unity Editor");
#endif
#if UNITY_STANDALONE_WIN
        Debug.Log("On Windows");
#endif

        //InvokeRepeating("CheckForControllerCount", 0, 0.3f);
    }

    // Update is called once per frame
    void Update ()
    {

        int i = 0;
        while (i < 4)
        {
            if (Mathf.Abs(Input.GetAxis("Joy" + i + "X")) > 0.2F)
                Debug.Log(Input.GetJoystickNames()[i] + i + " is moved in X");
            if (Mathf.Abs(Input.GetAxis("Joy" + i + "Y")) > 0.2F)
                Debug.Log(Input.GetJoystickNames()[i] + i + " is moved in Y");
            if (Mathf.Abs(Input.GetAxis("Fire" + i)) > 0)
                Debug.Log("Fire" + i);
            if (Mathf.Abs(Input.GetAxis("Melee" + i)) > 0)
                Debug.Log("Melee" + i);
            if (Input.GetButtonDown("Jump" + i))
                Debug.Log("Jump" + i);
            if (Input.GetButtonDown("Target" + i))
                Debug.Log("Target" + i);
            if (Input.GetButtonDown("CenterCam" + i))
                Debug.Log("Centering Camera " + i);
            if (Input.GetButtonDown("Alt1Fire" + i))
                Debug.Log("SubWeapon 1 firing from controller " + i);
            if (Input.GetButtonDown("Alt2Fire" + i))
                Debug.Log("SubWeapon 2 firing from controller " + i);
            i++;
        }
        
        CheckForControllerCount();
    }

    void CheckForControllerCount()
    {
        Debug.Log("Update");
        Debug.Log( Input.GetJoystickNames().IsSynchronized );
    }
}
