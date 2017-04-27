using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    // I'm well aware how horribly inefficient this is, I just need to test.
    [SerializeField]
    GameObject TestPlayerCam;

    [SerializeField]
    float FlightSpeed, Lifetime;

	// Use this for initialization
	void Start ()
    {
        FlightSpeed = 20f;
        Lifetime = 10f;

        //transform.eulerAngles = new Vector3(Screen.width / 2, Screen.height / 2 - 150);
        //transform.LookAt(TestPlayerCam.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(100f, 0.5f, 100)));

        transform.LookAt(TestPlayerCam.GetComponent<Camera>().ViewportToWorldPoint(GameObject.FindGameObjectWithTag("Reticle").transform.position));

        GetComponent<Rigidbody>().AddForce(Vector3.forward * FlightSpeed, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void FixedUpdate()
    {
        Lifetime -= Time.fixedDeltaTime * 1.5f;

        if (Lifetime <= 0) Destroy(gameObject);
    }
}
