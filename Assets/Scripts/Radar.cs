using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Radar : MonoBehaviour
{
    //public Transform CurrentLockOnTarget;
    bool _initialRadarPolled;
    public List<Transform> TargetsInRange;
    public Transform CurrentLockOnTarget { get; private set; }
    CameraMovement PlayerVisionReference;

    void Awake()
    {
        PlayerVisionReference = transform.parent.GetComponentInChildren<CameraMovement>();
    }

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("lock on target: " + CurrentLockOnTarget);
        if (CurrentLockOnTarget != null && (CurrentLockOnTarget.root.GetComponent<DamageableObject>() && !CurrentLockOnTarget.root.GetComponent<DamageableObject>().IsTargetable))
            DeactivateRadar();
	}

    public void PingRadar(LockOnState loState)
    {
        switch(loState)
        {
            case LockOnState.FREE:
                ActivateRadar();
                break;
            case LockOnState.LOCKED:
                DeactivateRadar();
                break;
        }
    }

    void ActivateRadar()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, 150f);

        foreach (Collider t in targets)
        {
            if (t.tag == "Controllable" && t.gameObject != gameObject)
                TargetsInRange.Add(t.gameObject.transform);
        }

        foreach (Transform t in TargetsInRange)
            Debug.Log(t.name);

        if (TargetsInRange.Count > 0)
        {
            AcquireLockOnTarget();
            PlayerVisionReference.SetCameraLockOnReference(CurrentLockOnTarget);
        }
        else
            ReportTargetAcquisitionFailure();
        
    }

    private void ReportTargetAcquisitionFailure()
    {
        CurrentLockOnTarget = null;
        Debug.Log("No targets in range.");
    }

    private void DeactivateRadar()
    {
        ClearEnemyList();
        CurrentLockOnTarget = null;
        PlayerVisionReference.SetCameraLockOnReference(CurrentLockOnTarget);
    }

    void AcquireLockOnTarget()
    {
        CurrentLockOnTarget = ReturnCorrectTargetByDistance();
        Debug.Log("Lock on target: " + CurrentLockOnTarget);
    }

    Transform ReturnCorrectTargetByDistance()
    {
        // Orders the enemy list from closest to farthest in terms of distance.
        // Magnitude is actually cheaper, so we might use that for optimization at a later point.
        if (TargetsInRange.Count > 1)
        {
            TargetsInRange = TargetsInRange.OrderBy(
                x => Vector3.Distance(transform.position, x.transform.position)
            ).ToList();
        }

        // Returns the first Transform in the enemy list, which is the closest one.
        return TargetsInRange[0];
    }

    public void ClearEnemyList()
    {
        TargetsInRange.Clear();
        Debug.Log("Enemy list cleared. " + TargetsInRange.Count + " enemies in list.");
    }
}
