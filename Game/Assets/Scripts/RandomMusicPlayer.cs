using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{
    public AudioClip[] musicTracks; // Масив з аудіокліпами
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomTrack();
    }

    void PlayRandomTrack()
    {
        int randomIndex = Random.Range(0, musicTracks.Length);
        audioSource.clip = musicTracks[randomIndex];
        audioSource.Play();

        // Викликайте цей метод використовуючи StartCoroutine, щоб музика постійно грала
        StartCoroutine(PlayNextRandomTrack());
    }

    IEnumerator PlayNextRandomTrack()
    {
        // Чекаємо, поки музика закінчиться
        yield return new WaitForSeconds(audioSource.clip.length);

        // Програваємо наступний випадковий трек
        PlayRandomTrack();
    }
}
