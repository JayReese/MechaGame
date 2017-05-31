using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// I'll be honest, I'm not even sure what this class does right now.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    Transform _currentTargetToTrack;

    bool LockOnTargetOutOfViewX, LockOnTargetOutOfViewY;
    float _horizontalTargetOffScreenBoundaryLowerLimit, _horizontalTargetOffScreenBoundaryUpperLimit; 

    // Use this for initialization
    void Start ()
    {
        #region Horizontal off screen boundaries.
        // When the relative position of the lock on target reaches this point on the negative side of the X-axis, the camera will register that the target is out of view.
        // The HIGHER this is, the more sensitive the horizontal reorient functionality is.
        _horizontalTargetOffScreenBoundaryLowerLimit = 0.2f;

        // When the relative position of lock on target reaches this point on the positive side of the X-axis, the camera will register that the target is out of view.
        // The LOWER this is, the more sensitive the reorient functionality is.
        _horizontalTargetOffScreenBoundaryUpperLimit = 0.8f;
        #endregion
    }

    // Update is called once per frame
    void Update ()
    {
        #region Defunct commented out code.
        /// DEFUCT, DONT WORRY ABOUT THIS.
        //transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        //transform.LookAt(new Vector3(TetheredPlayer.position.x, TetheredPlayer.position.y + 1.5f, TetheredPlayer.position.z));

        /// DEFUNCT, TRIED COMPLICATED SHIT AND DECIDED AGAINST IT. 
        //if (GeometryUtility.TestPlanesAABB(CameraPlanes, EnemyCollider.bounds))
        //    Debug.Log("There's something over there.");
        //else
        //    Debug.Log("There's nothing over there.");
        #endregion


        if (_currentTargetToTrack != null)
            CheckIfTargetIsOutOfRange();
        else
            Debug.Log("No lock on target");
    }

    void FixedUpdate()
    {
        if (LockOnTargetOutOfViewX)
            OrientToLockOnTarget();
    }

    public void SetCameraLockOnReference(Transform targetToLockOn)
    {
        _currentTargetToTrack = targetToLockOn;
    }

    private void OrientToLockOnTarget()
    {
        transform.parent.LookAt(_currentTargetToTrack);
    }

    private void CheckIfTargetIsOutOfRange()
    {
        LockOnTargetOutOfViewX = TargetOutOfFrustumViewX();
        LockOnTargetOutOfViewY = TargetOutOfFrustumViewY();

    }

    private bool TargetOutOfFrustumViewY()
    {
        // Not implemented.
        return false;
    }


    /// <summary>
    /// Checks periodically if the target is out of the viewport on the X-axis. It'll return a bool based on the calculations done.
    /// </summary>
    /// <returns>Bool</returns>
    private bool TargetOutOfFrustumViewX()
    {
        //Debug.Log("Pos: " + Math.Round(GetComponent<Camera>().WorldToViewportPoint(GameObject.Find(_currentTargetToTrack.name).transform.position).x, 1));

        if (Math.Round(GetComponent<Camera>().WorldToViewportPoint(GameObject.Find(_currentTargetToTrack.name).transform.position).x, 1) >= _horizontalTargetOffScreenBoundaryUpperLimit || Math.Round(GetComponent<Camera>().WorldToViewportPoint(GameObject.Find(_currentTargetToTrack.name).transform.position).x, 1) <= _horizontalTargetOffScreenBoundaryLowerLimit)
            return true;

        return false;
    }
}