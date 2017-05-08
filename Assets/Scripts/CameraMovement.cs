using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// I'll be honest, I'm not even sure what this class does right now.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform TetheredPlayer, CurrentLockOnTarget;
    [SerializeField]
    Plane[] CameraPlanes;
    [SerializeField]
    Collider EnemyCollider;
	
    // Use this for initialization
	void Start ()
    {
        TetheredPlayer = GameObject.FindGameObjectWithTag("Controllable").transform;
        //CameraPlanes = GeometryUtility.CalculateFrustumPlanes(GetComponent<Camera>());
        //EnemyCollider = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        /// DEFUCT, DONT WORRY ABOUT THIS.
        //transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        //transform.LookAt(new Vector3(TetheredPlayer.position.x, TetheredPlayer.position.y + 1.5f, TetheredPlayer.position.z));

        /// DEFUNCT, TRIED COMPLICATED SHIT AND DECIDED AGAINST IT. 
        //if (GeometryUtility.TestPlanesAABB(CameraPlanes, EnemyCollider.bounds))
        //    Debug.Log("There's something over there.");
        //else
        //    Debug.Log("There's nothing over there.");

        //if (CurrentLockOnTarget)
        //    Debug.Log(GetComponent<Camera>().WorldToViewportPoint(CurrentLockOnTarget.position).x);
            //Debug.Log(Math.Round(GetComponent<Camera>().WorldToViewportPoint(GameObject.Find(CurrentLockOnTarget.name).transform.position), 1));

    }

    public void CameraLockOn(Transform targetToLockOn)
    {
        Debug.Log(targetToLockOn);
        //transform.Rotate(new Vector3(targetToLockOn.rotation.x, targetToLockOn.rotation.y, transform.localEulerAngles.z));
        //CurrentLockOnTarget = GameObject.Find(targetToLockOn.name).transform;
        CurrentLockOnTarget = targetToLockOn;
    }
}