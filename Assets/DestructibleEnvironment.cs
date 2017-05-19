using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleEnvironment : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StructuralIntegrity = 10;
	}

    public int StructuralIntegrity { get; private set; }

    public void Degrade(int integrity)
    {
        if (StructuralIntegrity > 0) StructuralIntegrity -= integrity;

        else Destroy();
    }
    public void Destroy()
    {
        StructuralIntegrity = 0;

    }
    public void Reconstruct()
    {
        StructuralIntegrity = 10;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
