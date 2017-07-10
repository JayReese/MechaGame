using UnityEngine;
using System.Collections;

public class ArmorShield : ArmorPiece
{
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void OnEnable()
    {
        IsPersistingObject = false;
        DamageSurfaceType = SurfaceType.ARMOR;

        GetComponent<MeshRenderer>().material.color = new Color(GetComponent<MeshRenderer>().material.color.r, GetComponent<MeshRenderer>().material.color.g, GetComponent<MeshRenderer>().material.color.b, .1f);
    }
}
