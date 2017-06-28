using UnityEngine;
using System.Collections;

public class DamageableObject : MonoBehaviour
{
    public int Health;
    public bool IsTargetable, IsPersistingObject, IsPlayer;
    public float RespawnTimer;

    public SurfaceType DamageSurfaceType;

    public virtual void ReceiveDamage(int amount)
    {
        Health -= amount;

        if (Health <= 0) Kill();

        Debug.Log(string.Format("change to health detected on {0}, {1} HP left.", gameObject.name, this.Health));
    }

    protected void FixedUpdate()
    {
        //DecrementDestructTimer();
    }

    private void DecrementDestructTimer()
    {
        if (RespawnTimer <= 0) RespawnTimer -= Time.deltaTime;
    }

    public virtual void Kill( string g = "regular death")
    {
        Debug.Log(gameObject.name + " destroyed. " + g + " Health: " + Health);
        if(!IsPlayer) gameObject.SetActive(false);

        if (!IsTargetable && IsPersistingObject)
        {
            Debug.Log(gameObject.name + " destroyed. Health: " + Health);
            DecrementDestructTimer();
        }

        if (!IsPersistingObject)
            Kill("non pers obj killed.");
    }

    protected virtual void OnEnable()
    {
        if (gameObject.tag == "Controllable")
        {
            IsPlayer = true;
            RespawnTimer = 4;
        }
    }
}
