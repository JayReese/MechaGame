using UnityEngine;
using System.Collections;

public class ParticleExplosion : MonoBehaviour
{
    public float Lifetime;

	// Use this for initialization
	void Start ()
    {
        Lifetime = 2f;    
	}
	
	// Update is called once per frame
	void Update ()
    {
        Lifetime -= Time.deltaTime;

        if (Lifetime <= 0)
            Destroy(gameObject);
	}
}
