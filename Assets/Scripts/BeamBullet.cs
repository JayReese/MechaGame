using UnityEngine;
using System.Collections;

public class BeamBullet : MonoBehaviour {

    public Transform[] availableTeleportLocations;
    RaycastHit[] hits;
    public float DetectionRadius;
    public Transform SwapPosition;
    public GameObject PlayerToSwap;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //set up to use a layermask for the teleporter points (layer 9)
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Player>() == null)
        {
            hits = Physics.SphereCastAll(this.transform.position, DetectionRadius, transform.forward, 1 << 9);
            availableTeleportLocations = new Transform[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                availableTeleportLocations[i] = hits[i].transform;
            }
        }
        else
        {
            this.gameObject.transform.SetParent(col.transform);
            PlayerToSwap = col.gameObject;
        }
    }
}
