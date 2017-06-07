using UnityEngine;
using System.Collections;

public class DamageableObject : MonoBehaviour
{
    public int Health;
    public bool IsTargetable, IsPersistingObject;
    [SerializeField] protected float _destructTimer;

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
        if (!IsTargetable)
        {
            _destructTimer -= Time.fixedDeltaTime;

            if (_destructTimer <= 0 && IsPersistingObject)
                Kill();
        }

        if (!IsPersistingObject && Health <= 0)
            Kill();
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    protected virtual void OnEnable()
    {
        _destructTimer = IsPersistingObject ? 4 : 0;
    }
}
