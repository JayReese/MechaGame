using UnityEngine;
using System.Collections;

public class TrainingBot : LiveEntity
{
    [SerializeField] float direction, lifetime;
    [SerializeField]
    GameObject bulletP;
    int ammo, maxAmmo;

	// Use this for initialization
	void Start ()
    {
        maxAmmo = 10;
        ammo = maxAmmo;

        lifetime = 4;
        direction = Random.Range(1, 100) > 50 ? 1 : -1;
        bulletP = Resources.Load("Prefabs/Test Projectile") as GameObject;
        //InvokeRepeating("Fire", 0, 0.4f);
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
        //Debug.Log(c.tag);
    }

    void Fire()
    {
        GameObject g = bulletP;

        g.GetComponent<Projectile>().PlayerOrigin = transform;

        Instantiate(g, transform.FindChild("Emitter").position, transform.FindChild("Emitter").rotation);
    }
}
