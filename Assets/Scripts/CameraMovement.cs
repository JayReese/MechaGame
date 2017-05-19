using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// I'll be honest, I'm not even sure what this class does right now.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform TetheredPlayer;
    public Transform CurrentLockOnTarget { get; private set; }
    //private bool _cameraLock;
    public bool LockOnTargetOutOfView;

    // Use this for initialization
    void Start ()
    {
        TetheredPlayer = GameObject.FindGameObjectWithTag("Controllable").transform;
        //ursulaDewitt = true;
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

        //if (CurrentLockOnTarget)
        //    //    Debug.Log(GetComponent<Camera>().WorldToViewportPoint(CurrentLockOnTarget.position).x);
        //    Debug.Log(string.Format("{0}: {1}", (int)Vector3.Angle(transform.position, CurrentLockOnTarget.position), Math.Round(GetComponent<Camera>().WorldToViewportPoint(GameObject.Find(CurrentLockOnTarget.name).transform.position).x, 1)));

        if (CurrentLockOnTarget)
        {
            CheckIfTargetIsOutOfRange();
            //Debug.Log(Math.Round(GetComponent<Camera>().WorldToViewportPoint(CurrentLockOnTarget.position).x, 1));
        }

        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 90, 0), Time.deltaTime * 5f);
    }

    void FixedUpdate()
    {
        //if(CurrentLockOnTarget)
        //    CheckIfTargetIsOutOfRange();
    }

    public void CameraLockOn(Transform targetToLockOn)
    {
        Debug.Log(targetToLockOn);

        #region Commented out - old method of acquiring targets
        //transform.Rotate(new Vector3(targetToLockOn.rotation.x, targetToLockOn.rotation.y, transform.localEulerAngles.z));
        //CurrentLockOnTarget = GameObject.Find(targetToLockOn.name).transform;
        #endregion

        CurrentLockOnTarget = targetToLockOn;
    }

    private void CheckIfTargetIsOutOfRange()
    {
       LockOnTargetOutOfView = TargetOutOfFrustumView();
    }


    /// <summary>
    /// Checks periodically if the target is out of the viewport. It'll return a bool based on the calculations done.
    /// </summary>
    /// <returns>Bool</returns>
    private bool TargetOutOfFrustumView()
    {
        if (Math.Round(GetComponent<Camera>().WorldToViewportPoint(GameObject.Find(CurrentLockOnTarget.name).transform.position).x, 1) >= 1.0f || Math.Round(GetComponent<Camera>().WorldToViewportPoint(GameObject.Find(CurrentLockOnTarget.name).transform.position).x, 1) <= 0)
            return true;

        return false;
    }
}