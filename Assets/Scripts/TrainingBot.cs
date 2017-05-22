using UnityEngine;
using System.Collections;

public class TrainingBot : LiveEntity
{
    [SerializeField] float direction, lifetime;

	// Use this for initialization
	void Start ()
    {
        lifetime = 4;
        direction = Random.Range(1, 100) > 50 ? 1 : -1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        lifetime -= Time.deltaTime * 1.5f;

        if (lifetime <= 0)
        {
            lifetime = 4;
            direction *= -1;
        }

        transform.position += (transform.right * direction) * 10f * Time.deltaTime;
	}

    void OnTriggerEnter(Collider c)
    {
        Debug.Log(c.tag);
    }
}
