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

    #region Player-oriented methods.
    protected virtual void ToggleLife()
    {
        if (RespawnTimer <= 0) RespawnTimer -= Time.deltaTime;
    }

    protected virtual void OnEnable()
    {
        if (gameObject.tag == "Controllable")
        {
            IsPlayer = true;
            RespawnTimer = 4;
        }
    }
    #endregion
}
