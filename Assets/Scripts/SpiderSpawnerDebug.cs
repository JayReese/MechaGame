using UnityEngine;
using System.Collections;

public class SpiderSpawnerDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Resources.Load("Prefabs/Testing/Spider"), this.transform.position, this.transform.rotation);
        }
    }
}
