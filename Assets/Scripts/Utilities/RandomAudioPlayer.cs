using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomAudioPlayer : MonoBehaviour
{
    protected AudioSource audioSource;
    public AudioClip[] clips = new AudioClip[3];
    public bool randomizePitch = true;
    [Range(0.0f, 1.0f)]
    public float pitchRange = 0.3f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        int clipChoice = Random.Range(0, clips.Length);

        if (randomizePitch)
            audioSource.pitch = Random.Range(1.0f - pitchRange, 1.0f + pitchRange);

        audioSource.PlayOneShot(clips[clipChoice]);
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
