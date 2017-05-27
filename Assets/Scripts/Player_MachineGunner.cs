using UnityEngine;
using System.Collections;

public class Player_MachineGunner : Player
{

    // Use this for initialization
    new void Start()
    {
        FirstSubWeaponCooldown = 5f;
        SecondSubWeaponCooldown = 5f;

        Health = 10;

        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void UseFirstSubweapon()
    {
        Debug.Log("Machi First Subweapon Used");
        base.UseFirstSubweapon();
    }

    protected override void UseSecondSubweapon()
    {
        Debug.Log("Machi Second Subweapon Used");
        base.UseSecondSubweapon();
    }
}
