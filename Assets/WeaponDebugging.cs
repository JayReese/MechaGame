using UnityEngine;
using System.Collections;
using System;

public class WeaponDebugging : MonoBehaviour
{

    [SerializeField]
    Transform LockOnTarget;
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField]
    float Timer, Accuracy, ShotsFired, ShotsHit;
    [SerializeField]
    int TestNumber;

    public bool IsTesting;

	// Use this for initialization
	void Start ()
    {
        Timer = 30f;
        LockOnTarget = GameObject.Find("Target").transform;
	    _projectilePrefab = Resources.Load("Prefabs/Testing/Test Projectile") as GameObject;

        _projectilePrefab.GetComponent<Projectile>().FlightSpeed = 300f;
        _projectilePrefab.GetComponent<Projectile>().LockOnTarget = LockOnTarget;
        _projectilePrefab.GetComponent<Projectile>().wdb = this;
        _projectilePrefab.GetComponent<Projectile>().ProjectileLockOnWindow = 0.1f;

        IsTesting = true;

        if(IsTesting)
        {
            TestNumber = 1;
            InvokeRepeating("FireBullet", 0, 1f);
        }

        //Debug.Log(LockOnTarget.position.z / transform.position.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (IsTesting)
            RunTest();
        else
        {
            if (Input.GetMouseButtonDown(0))
                FireBullet();
        }
	}

    void RunTest()
    {
        if (TestNumber < 4)
            Timer -= Time.deltaTime;

        if (Timer <= 0)
            EndTest();

        if (TestNumber > 3)
            CancelInvoke("FireBullet");
    }

    private void EndTest()
    {
        Accuracy = (ShotsHit / ShotsFired) * 100f;

        Debug.Log(string.Format("TEST {0}: {1}", TestNumber, Accuracy));

        ShotsFired = 0;
        ShotsHit = 0;

        Timer = 30f;

        if (TestNumber != 3) TestNumber++;
        else Debug.Log("Tests over.");
    }

    void FireBullet()
    {
        ShotsFired++;
        Instantiate(_projectilePrefab, transform.position, transform.rotation);
    }

#if UNITY_EDITOR
    public void LogResult(bool isHit)
    {
        ShotsHit = isHit ? ShotsHit + 1 : ShotsHit;
    }
#endif
}
