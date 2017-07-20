using UnityEngine;
using System.Collections;

public class TargetDebugging : MonoBehaviour
{
    [SerializeField] float direction, life;

	// Use this for initialization
	void Start ()
    {
        life = 4.0f;
        direction = 1f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (life > 0)
            life -= Time.deltaTime;
        else
        {
            direction *= -1;
            life = 4.0f;
        }

        //transform.position += transform.right * direction * Time.deltaTime * 10f;
    }
}
