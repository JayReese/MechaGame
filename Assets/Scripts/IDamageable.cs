using UnityEngine;
using System.Collections;

public interface IDamageable
{
    /*
     * PURPOSE: Was originally here for polymorphism - now it's here to stay because an IDamageable is able to receive damage dealt by an object that deals it.
     * It's still very polymorphic, though, and that's the good thing.
     */
    
    /// <summary>
    /// The object implementing the ReceiveDamage method is able to take damage from other objects.
    /// </summary>
    /// <param name="amount"></param>
    void ReceiveDamage(int amount);
}
