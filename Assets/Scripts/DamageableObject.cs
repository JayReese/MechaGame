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
        Debug.Log(gameObject.name + " destroyed. " + g);
    }

    protected virtual void OnEnable()
    {
        _destructTimer = IsPersistingObject ? 4 : 0;
    }
}
