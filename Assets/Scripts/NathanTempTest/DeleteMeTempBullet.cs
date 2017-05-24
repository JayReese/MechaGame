using UnityEngine;
using System.Collections;

public class DeleteMeTempBullet : MonoBehaviour
{
    [SerializeField] float damageToDeal = 25f;
    [SerializeField] float launchVelocity = 10f;

    DeleteMeTempController playerSource;
    Rigidbody myRigidBody;

    //makeshift constructor (MUST BE CALLED FROM PLAYER)
    public void Init(DeleteMeTempController desiredPlayerSource)
    {
        playerSource = desiredPlayerSource;

        myRigidBody = GetComponent<Rigidbody>();
        myRigidBody.velocity = transform.forward * launchVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        DeleteMeTempController hitPlayer = collision.gameObject.GetComponent<DeleteMeTempController>();

        if (hitPlayer != null && hitPlayer.TeamIndex != playerSource.TeamIndex) //second condition prevents friendly fire.  Each bullet can obviously have different "OnCollisionEnter"s
        {
            //deal damage.  And, if a player got a kill...
            if(hitPlayer.TakeDamageAndDetectKill(damageToDeal))
            {
                playerSource.AlertKill(hitPlayer);
            }

            //update source player's accuracy (you got a hit!)

            //add a delay to play sound effects and/or visual effects
            Destroy(this.gameObject);
        }
    }
}
