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
        Debug.Log(string.Format("change to health detected on {0}, {1} HP left.", gameObject.name, this.Health)); 
    }

    protected void FixedUpdate()
    {
        //DecrementDestructTimer();
        ToggleLife();
    }

    private void DecrementDestructTimer()
    {
        //if (!IsTargetable && IsPersistingObject)
        //{
        //    Debug.Log(gameObject.name + " destroyed. Health: " + Health);
        //    RespawnTimer -= Time.fixedDeltaTime;

        //    if (RespawnTimer <= 0 && IsPersistingObject)
        //        Kill();
        //}

        //if (!IsPersistingObject && Health <= 0)
        //    Kill("non pers obj killed.");
    }

    #region Player-oriented methods.
    protected virtual void ToggleLife()
    {
        
    }
    #endregion
}
