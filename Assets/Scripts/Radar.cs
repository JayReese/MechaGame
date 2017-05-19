using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Radar : MonoBehaviour
{
    List<Transform> Enemies;
    bool _initialRadarPolled;

	// Use this for initialization
	void Start ()
    {
        _initialRadarPolled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void Activate(List<Transform> enemies, ref Transform lockOnTarget)
    {
        Debug.Log("Radar activated");
        
        StartCoroutine(ToggleRadarCollider());
        GetLockOnTarget(enemies, ref lockOnTarget);
    }

    IEnumerator ToggleRadarCollider()
    {
        GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider>().enabled = false;
    }

    void OnTriggerEnter(Collider c)
    {
        //Debug.Log(c.name);
    }

    void GetLockOnTarget(List<Transform> enemies, ref Transform lockOnTarget)
    {
        //lockOnTarget = ReturnCorrectTargetByDistance(enemies);
        if (enemies.Count > 0)
            lockOnTarget = ReturnCorrectTargetByDistance(enemies);
        else
            StartCoroutine(ToggleRadarCollider());
    }

    Transform ReturnCorrectTargetByDistance(List<Transform> enemies)
    {
        // Orders the enemy list from closest to farthest in terms of distance.
        // Magnitude is actually cheaper, so we might use that for optimization at a later point.
        enemies = enemies.OrderBy(
            x => Vector3.Distance(transform.position, x.transform.position)
            ).ToList();

        // Returns the first Transform in the enemy list, which is the closest one.
        return enemies[0];
    }

    public void BeginDefaults(List<Transform> initialEnemyList)
    {
        if (!_initialRadarPolled)
        {
            // Starts the radar from the very beginning. Something of a band-aid, but it works reusably.
            StartCoroutine(ToggleRadarCollider());

            // Clears out the enemy list.
            ClearEnemyList(initialEnemyList);

            _initialRadarPolled = true;
        }
        else
            Debug.Log("Radar already polled, stop trying this.");
    }

    public void ClearEnemyList(List<Transform> enemyList)
    {
        enemyList.Clear();
        Debug.Log("Enemy list cleared. " + enemyList.Count + " enemies in list.");
    }
}
