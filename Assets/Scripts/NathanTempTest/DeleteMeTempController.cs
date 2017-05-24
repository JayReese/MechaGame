using UnityEngine;
using System.Collections;

public class DeleteMeTempController : MonoBehaviour
{
    /*
     * This class is intended to be a temporary class used for testing purposes.
     * However, the code's logic might be salvagable in some cases
     * 
     * Death cycle:
     * FirstTimeSetup -> Start -> [Init -> TakeDamage -> Die] -> Init...
     * 
     * Bullet notifies when a player is hit or killed
     */

    [SerializeField] float startingHealth = 100;
    [SerializeField] GameObject bulletPrefab;

    int teamIndex;
    public int TeamIndex { get { return teamIndex; } }

    float currentHealth;
    Transform[] startingPositions;
    GameManager gameManager;

    int kills;
    int deaths;

    void Start()
    {
        //do MOAR stuff
        kills = 0;
        deaths = 0;

        Init();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
    }

    void Init()
    {
        //set the player's spawn location to a random starting location on the map
        //granted, this will need to be wiped and made more intricate in the future
        transform.position = startingPositions[Random.Range(0, (int)startingPositions.Length)].position;

        currentHealth = startingHealth;
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<DeleteMeTempBullet>().Init(this);
    }

    void Die()
    {
        //have some sort of death delay (IEnumerable / code timer in update)
        deaths++;
        Init();
    }

    /// <summary>
    /// Makeshift constructor
    /// </summary>
    /// <param name="gm">References the gameManager (to update team scores)</param>
    /// <param name="_teamIndex">which team number this player is a part of</param>
    /// <param name="_startingPositions">which locations can this player spawn at when it dies?</param>
    public void FirstTimeSetup(GameManager gm, int _teamIndex, Transform[] _startingPositions)
    {
        gameManager = gm;
        teamIndex = _teamIndex;
        startingPositions = _startingPositions;
    }

    public void AlertKill(DeleteMeTempController playerKilled)
    {
        kills++;
        gameManager.AdjustTeamPoints(GameManager.pointsPerKill, teamIndex);
    }

    public bool TakeDamageAndDetectKill(float amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Die();
            return true;
        }

        return false;
    }
}
