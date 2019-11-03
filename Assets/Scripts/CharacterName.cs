using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class CharacterName : MonoBehaviour
{
    static protected CharacterName s_PlayerInstance;
    static public CharacterName PlayerInstance { get { return s_PlayerInstance; } }

    [Header("Audio")]
    public RandomAudioPlayer hurtSFX;

    void Awake()
    {
        s_PlayerInstance = this;
    }

        // Start is called before the first frame update
        void Start()
    {

    }

    // Update is called once per frame 
    void Update()
    {

    }

    public void OnHurt()
    {
        hurtSFX.PlayRandomSound();
    }

    void Die()
    {
        print("I'm dead");
    }
}
