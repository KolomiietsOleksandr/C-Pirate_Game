using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip soundClipHit; 
    public AudioClip soundClipCRHit; 
    public AudioClip OceanSoundClip; 
    private AudioSource audioSource;
    public GameObject OceanSound;
    public int x = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SoundHit()
    {
        audioSource.PlayOneShot(soundClipHit);
    }  
    
    public void SoundCRHit()
    {
        audioSource.PlayOneShot(soundClipCRHit);
    }

    public void Update()
    {
        if (OceanSound != null && x == 0)
        {
            audioSource.PlayOneShot(OceanSoundClip);
            x = 1;
        }
    }
}
