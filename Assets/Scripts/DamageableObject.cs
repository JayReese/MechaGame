using UnityEngine;
using System.Collections;

public class DamageableObject : MonoBehaviour
{

    public bool IsTargetable;
    [SerializeField] protected float _destructTimer;

    public virtual void ReceiveDamage(int amount)
    {

    }

    protected void FixedUpdate()
    {
        DecrementDestructTimer();
    }

    private void DecrementDestructTimer()
    {
        if (!IsTargetable)
            _destructTimer -= Time.fixedDeltaTime;
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
