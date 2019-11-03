using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    public int damage = 1;
    public bool canHitTriggers;
    public LayerMask hitableLayer;
    public bool ignoreInvincibility;
    public UnityEvent onDamageableHit;
    public UnityEvent onNonDamageableHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable dmg = other.gameObject.GetComponent<Damageable>();

        if (dmg != null)
        {
            dmg.TakeDamage(this);
            onDamageableHit.Invoke();
        }
        else
        {
            onNonDamageableHit.Invoke();
        }
    }
}
