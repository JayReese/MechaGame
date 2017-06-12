using UnityEngine;
using System.Collections;

public class ArmorShield : ArmorPiece
{
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    protected override void OnEnable()
    {
        IsPersistingObject = false;
        DamageSurfaceType = SurfaceType.ARMOR;

        base.OnEnable();
    }
}
