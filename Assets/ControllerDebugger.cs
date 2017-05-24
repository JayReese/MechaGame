using UnityEngine;
using System.Collections;

public class ControllerDebugger : MonoBehaviour {

<<<<<<< HEAD
    public int ControllerCount;

=======

    
>>>>>>> e735f92f6e31da9b46cfdc6560fabc0fccc96b48
	// Use this for initialization
	void Start ()
    {
#if UNITY_EDITOR
        Debug.Log("This is Unity Editor");
#endif
#if UNITY_STANDALONE_WIN
        Debug.Log("On Windows");
#endif

<<<<<<< HEAD
        //InvokeRepeating("CheckForControllerCount", 0, 0.3f);
=======
        
>>>>>>> e735f92f6e31da9b46cfdc6560fabc0fccc96b48
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
