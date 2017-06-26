using UnityEngine;
using System.Collections;

public class EntityTesting : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("Armor");

        for (byte b = 0; b < a.Length; b++)
            a[b].GetComponent<MeshRenderer>().material.color = Color.black;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
