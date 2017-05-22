using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{ 
    [SerializeField]
    public Transform Origin, LockOnTarget;

    [SerializeField]
    float FlightSpeed, Lifetime;

    [SerializeField]
    bool PlayerIsLockedOn;

	// Use this for initialization
	void Start ()
    {
        FlightSpeed = 60f;
        Lifetime = 10f;

        PlayerIsLockedOn = LockOnTarget != null ? true : false;

        if (!PlayerIsLockedOn)
            GetComponent<Rigidbody>().AddForce(transform.forward * FlightSpeed, ForceMode.Impulse);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (PlayerIsLockedOn)
            transform.position = Vector3.MoveTowards(transform.position, LockOnTarget.position, FlightSpeed * Time.fixedDeltaTime);
        else
            DegradeProjectileLife();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != Origin) Destroy(gameObject);
    }

    void DegradeProjectileLife()
    {
        Lifetime -= Time.fixedDeltaTime * 1.5f;

        if (Lifetime <= 0) Destroy(gameObject);
    }
}
