using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{
    public AudioClip[] musicTracks; // ����� � ����������
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

        // ���������� ��� ����� �������������� StartCoroutine, ��� ������ ������� �����
        StartCoroutine(PlayNextRandomTrack());
    }

    IEnumerator PlayNextRandomTrack()
    {
        // ������, ���� ������ ����������
        yield return new WaitForSeconds(audioSource.clip.length);

        // ���������� ��������� ���������� ����
        PlayRandomTrack();
    }
}
