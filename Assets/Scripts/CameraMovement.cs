using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// I'll be honest, I'm not even sure what this class does right now.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform _currentTargetToTrack;
    Camera PlayerCameraReference;
    Vector3 _defaultCameraRotation;

    [SerializeField] bool LockOnTargetOutOfViewX, LockOnTargetOutOfViewY;
    [SerializeField] float _horizontalTargetOffScreenBoundaryLowerLimit, _horizontalTargetOffScreenBoundaryUpperLimit, 
          _verticalTargetOffScreenBoundaryLowerLimit, _verticalTargetOffScreenBoundaryUpperLimit; 

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

        #region Vertical off screen boundaries.
        // When the relative position of the lock on target reaches this point on the negative side of the Y-axis, the camera will register that the target is out of view.
        // The HIGHER this is, the more sensitive the horizontal reorient functionality is.
        _verticalTargetOffScreenBoundaryLowerLimit = 0f;

        // When the relative position of lock on target reaches this point on the positive side of the Y-axis, the camera will register that the target is out of view.
        // The LOWER this is, the more sensitive the reorient functionality is.
        _verticalTargetOffScreenBoundaryUpperLimit = 1f;
        #endregion

        _defaultCameraRotation = transform.localEulerAngles;

        PlayerCameraReference = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (_currentTargetToTrack != null)
            CheckIfTargetIsOutOfRange();
    }

    void FixedUpdate()
    {
        if (LockOnTargetOutOfViewX || LockOnTargetOutOfViewY)
            OrientToLockOnTarget();
    }

    public void SetCameraLockOnReference(Transform targetToLockOn)
    {
        _currentTargetToTrack = targetToLockOn;
    }

    private void OrientToLockOnTarget()
    {
        //transform.parent.LookAt(_currentTargetToTrack);

        //_currentLockOnTargetPosition = _currentTargetToTrack.position;
        //_currentLockOnTargetPosition.y = transform.parent.GetComponent<Player>().IsOnGround ? 0.0f : _currentLockOnTargetPosition.y;

        transform.parent.LookAt(_currentTargetToTrack);
    }

    private void CheckIfTargetIsOutOfRange()
    {
        LockOnTargetOutOfViewX = TargetOutOfFrustumViewX();
        LockOnTargetOutOfViewY = TargetOutOfFrustumViewY();
    }

    /// <summary>
    /// Checks periodically if the target is out of the viewport on the Y-axis. It'll return a bool based on the calculations done.
    /// </summary>
    /// <returns>Bool</returns>
    private bool TargetOutOfFrustumViewY()
    {
        //Debug.Log("Pos: " + Math.Round(GetComponent<Camera>().WorldToViewportPoint(GameObject.Find(_currentTargetToTrack.name).transform.position).y, 1));

        if (Math.Round(PlayerCameraReference.WorldToViewportPoint(GameObject.Find(_currentTargetToTrack.name).transform.position).y, 1) >= _verticalTargetOffScreenBoundaryUpperLimit || Math.Round(PlayerCameraReference.WorldToViewportPoint(GameObject.Find(_currentTargetToTrack.name).transform.position).y, 1) <=  _verticalTargetOffScreenBoundaryLowerLimit)
            return true;

        return false;
    }


    /// <summary>
    /// Checks periodically if the target is out of the viewport on the X-axis. It'll return a bool based on the calculations done.
    /// </summary>
    /// <returns>Bool</returns>
    private bool TargetOutOfFrustumViewX()
    {
        //Debug.Log("Pos: " + Math.Round(GetComponent<Camera>().WorldToViewportPoint(GameObject.Find(_currentTargetToTrack.name).transform.position).x, 1));

        if (Math.Round(PlayerCameraReference.WorldToViewportPoint(GameObject.Find(_currentTargetToTrack.name).transform.position).x, 1) >= _horizontalTargetOffScreenBoundaryUpperLimit || Math.Round(PlayerCameraReference.WorldToViewportPoint(GameObject.Find(_currentTargetToTrack.name).transform.position).x, 1) <= _horizontalTargetOffScreenBoundaryLowerLimit)
            return true;

        return false;
    }

    public void ReorientToCenter()
    {
        transform.localEulerAngles = _defaultCameraRotation;
    }
} 