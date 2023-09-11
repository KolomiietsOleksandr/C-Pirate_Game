using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundClickHit;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SoundHit()
    {
        audioSource.PlayOneShot(soundClickHit);
    }
}
