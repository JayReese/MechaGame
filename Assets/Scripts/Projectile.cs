using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    // I'm well aware how horribly inefficient this is, I just need to test.
    [SerializeField]
    GameObject TestPlayerCam;

    [SerializeField]
    public Transform Origin;

    [SerializeField]
    float FlightSpeed, Lifetime;

	// Use this for initialization
	void Start ()
    {
        FlightSpeed = 60f;
        Lifetime = 10f;

        //transform.eulerAngles = new Vector3(Screen.width / 2, Screen.height / 2 - 150);
        //transform.LookAt(TestPlayerCam.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(100f, 0.5f, 100)));

        TestPlayerCam = GameObject.FindGameObjectWithTag("PlayerCamera");

        GetComponent<Rigidbody>().AddForce(transform.forward * FlightSpeed, ForceMode.Impulse);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != Origin) Destroy(gameObject);
    }
}
