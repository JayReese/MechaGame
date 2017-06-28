using UnityEngine;
using System.Collections;
using System;

public class Player_BeamRifleman : Player
{
    //I'm not sure about the way I've (Jake) set this up because the only way I could think to get a value for hitLocation is for the bullet to reference the player that shoots it, making it super inefficient/sysem heavy
    [SerializeField]
    BeamBullet BeamReifleBullet;
    float distanceToClosestTelePoint;
    [SerializeField]
    Transform LocationToTeleportTo;
    Transform[] PossibleTeleportaionPoints;
    
    // Use this for initialization
    new void Awake()
    {
        base.Awake();
    }

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
        PerformCommandExecution(); CheckIfUsingMelee(); ManageCooldownTimers(); base.FixedUpdate();
    }

    protected override void UseFirstSubweapon()
    {
        Debug.Log("Beam First Subweapon Used");
        //check if there are available locations
        if (BeamReifleBullet.availableTeleportLocations == null) return;
        else //if there are then get all the points possible
        {
            PossibleTeleportaionPoints = BeamReifleBullet.availableTeleportLocations;
            //set the first point in array to teleport location for a comparision
            LocationToTeleportTo = PossibleTeleportaionPoints[0].transform; distanceToClosestTelePoint = (LocationToTeleportTo.position - BeamReifleBullet.transform.position).magnitude;
        }
        //determine closest teleport location
        FirstSubWeapon_GetClosestTeleportLoc();
        base.UseFirstSubweapon();
    }

    private void FirstSubWeapon_GetClosestTeleportLoc()
    {
        if (BeamReifleBullet.availableTeleportLocations != null)
        {
            for (int i = 0; i < PossibleTeleportaionPoints.Length; i++)
            {
                float tempDistance = (BeamReifleBullet.transform.position - PossibleTeleportaionPoints[i].transform.position).magnitude;
                if (tempDistance < distanceToClosestTelePoint) { LocationToTeleportTo = PossibleTeleportaionPoints[i].transform; }
            }
        }
    }

    protected override void UseSecondSubweapon()
    {
        Debug.Log("Beam Second Subweapon Used");
        if (BeamReifleBullet.PlayerToSwap != null)
        {
            Transform temp = this.gameObject.transform;
            this.transform.position = BeamReifleBullet.PlayerToSwap.transform.position;
            BeamReifleBullet.PlayerToSwap.transform.position = temp.position;
        }
        base.UseSecondSubweapon();
    }


}
