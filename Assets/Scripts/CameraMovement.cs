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
        TetheredPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        //CameraPlanes = GeometryUtility.CalculateFrustumPlanes(GetComponent<Camera>());
        //EnemyCollider = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        /// DEFUCT, DONT WORRY ABOUT THIS.
        //transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        //transform.LookAt(new Vector3(TetheredPlayer.position.x, TetheredPlayer.position.y + 1.5f, TetheredPlayer.position.z));

        LockOn();

        /// DEFUNCT, TRIED COMPLICATED SHIT AND DECIDED AGAINST IT. 
        //if (GeometryUtility.TestPlanesAABB(CameraPlanes, EnemyCollider.bounds))
        //    Debug.Log("There's something over there.");
        //else
        //    Debug.Log("There's nothing over there.");

        Debug.Log(Math.Round(GetComponent<Camera>().WorldToViewportPoint(GameObject.FindGameObjectWithTag("Enemy").transform.position).y, 1));
    }

    public void LockOn()
    {
        
    }
}
