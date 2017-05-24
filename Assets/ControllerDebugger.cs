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
