using UnityEngine;
using System.Collections;

public class DamageableObject : MonoBehaviour
{
    public int Health;
    public bool IsTargetable, IsPersistingObject;
    [SerializeField] protected float _destructTimer;

    public SurfaceType DamageSurfaceType;

    public virtual void ReceiveDamage(int amount)
    {
        Health -= amount;
        Debug.Log(string.Format("change to health detected on {0}, {1} HP left.", gameObject.name, this.Health)); 
    }

    protected void FixedUpdate()
    {
        DecrementDestructTimer();
    }

    private void DecrementDestructTimer()
    {
        if (!IsTargetable && IsPersistingObject)
        {
            _destructTimer -= Time.fixedDeltaTime;

            if (_destructTimer <= 0 && IsPersistingObject)
                Kill();
        }

        if (!IsPersistingObject && Health <= 0)
            Kill("non pers obj killed.");
    }

    private void Kill(string g = "regular death")
    {
        Debug.Log(gameObject.name + " destroyed. " + g + " Health: " + Health);
        gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        _destructTimer = IsPersistingObject ? 4 : 0;
    }
}
