using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Damageable : MonoBehaviour
{
    [Header("Health")]
    public int startingHP;
    public int maxHP;
    public int currentHP;
    public Collider2D hitbox;

    [Header("Damage settings")]
    public bool isInvincibleAfterHit;
    [Tooltip("Seconds")]
    public float invincibleDuration;
    public bool isInvincible;
    
    [Header("Events")]
    public UnityEvent whenHealthHasChanged;
    public UnityEvent whenDamaged;
    public bool disableOnDeath;
    public UnityEvent whenHealthIs0;
    // enum for disable or destory on death? 

    private void Awake()
    {
        currentHP = startingHP;
    }

    public void TakeDamage(Damager damager)
    {
        if (!isInvincible)
        {
            currentHP -= damager.damage;
            whenDamaged.Invoke();
        
            // Object is "dead"
            if (currentHP <= 0)
                whenHealthIs0.Invoke();

            ChangeHealth(damager.damage);
        }
    }

    public void GainHealth(int amount)
    {
        currentHP += amount;

        // Don't go above the max
        if (currentHP > maxHP)
            currentHP = startingHP;

        ChangeHealth(amount);
    }

    public void ChangeHealth(int amount)
    {
        // A callback for things that are listening, like UIs
        whenHealthHasChanged.Invoke();
    }
}
