using UnityEngine;
using System.Collections;

public class WeaponDebugging : MonoBehaviour
{

    [SerializeField]
    Transform LockOnTarget;
    [SerializeField] GameObject _projectilePrefab;

	// Use this for initialization
	void Start ()
    {
        LockOnTarget = GameObject.Find("Target").transform;
	    _projectilePrefab = Resources.Load("Prefabs/Testing/Test Projectile") as GameObject;

        _projectilePrefab.GetComponent<Projectile>().FlightSpeed = 5f;
        _projectilePrefab.GetComponent<Projectile>().LockOnTarget = LockOnTarget;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
            Instantiate(_projectilePrefab, transform.position, transform.rotation);
	}
}
