using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;
    public RandomAudioPlayer collectionSFX;
    public UnityEvent whenCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == CharacterName.PlayerInstance.gameObject)
        {
            other.gameObject.GetComponent<Damageable>().GainHealth(healAmount);
            whenCollected.Invoke();
        }
    }
}
