using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Radar : MonoBehaviour
{
    public List<Transform> Enemies { get; private set; }

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void Activate(List<Transform> enemies, ref Transform lockOnTarget)
    {
        Debug.Log("Radar activated");
        
        StartCoroutine(ToggleRadarCollider(enemies));
    }

    IEnumerator ToggleRadarCollider(List<Transform> enemies)
    {
        GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider>().enabled = false;

        GetLockOnTarget(enemies);
    }

    void OnTriggerEnter(Collider c)
    {
        //Debug.Log(c.name);
    }

    void GetLockOnTarget(List<Transform> enemies)
    {
        //lockOnTarget = ReturnCorrectTargetByDistance(enemies);
        if (enemies.Count > 0)
            Debug.Log(string.Format("Lock on target: {0}", enemies[0]));
        else
            Debug.Log("less enemies: " + enemies.Count);
    }

    Transform ReturnCorrectTargetByDistance(List<Transform> enemies)
    {
        #region Commented out - doesn't actually get the distance correctly, needs to be tested more.
        //byte counter = 0;
        //byte closestTally = 0;

        //if (enemies.Count > 0)
        //{
        //    for (byte i = 0; i < enemies.Count; i++)
        //    {
        //        if (i == counter)
        //            continue;

        //        if (Vector3.Distance(enemies[counter].position, gameObject.transform.position) < Vector3.Distance(enemies[i].position, gameObject.transform.position))
        //            closestTally++;

        //        if (closestTally == 2)
        //            break;
        //        else
        //        {
        //            closestTally = 0;
        //            counter++;
        //        }

        //    }
        //}


        //Debug.Log(string.Format("{0}: {1} | {2}: {3} | {4}: {5}", enemies[0].name, Vector3.Distance(enemies[0].position, gameObject.transform.position),
        //    enemies[1].name, Vector3.Distance(enemies[1].position, gameObject.transform.position),
        //    enemies[2].name, Vector3.Distance(enemies[2].position, gameObject.transform.position)));
        //Debug.Log(enemies[counter]);

        //return enemies[counter];
        #endregion

        // The temporary solution, just to make sure it compiles.
        return enemies[0];
    }
}
