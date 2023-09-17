using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSr : MonoBehaviour
{
    public string[] Ui;
    public AudioSource audioSourceMusic;
    public GameObject SoundsCross;
    public GameObject MusicCross;
    private GameObject CurrentUi;
    public bool OnOff = true;


    public void Update()
    {
        TurnOnOffAllAudioSources();
    }


    public void OnSettingButtonClickOn()
    {
        foreach (string obj in Ui)
        {
            CurrentUi = GameObject.Find($"{obj}");
            if (CurrentUi != null)
            {
                CurrentUi.SetActive(false);
            }
        }
    }


    public void MusicOnOff()
    {
        if (audioSourceMusic.enabled == false)
        {
            audioSourceMusic.enabled = true;
            MusicCross.SetActive(false);
        }
        else
        {
            audioSourceMusic.enabled = false;
            MusicCross.SetActive(true);
        }
    }


    public void TurnOnOffAllAudioSources()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name != "Main Camera")
            {
                AudioSource audioSourceS = obj.GetComponent<AudioSource>();

                if (audioSourceS != null)
                {
                    if (audioSourceS.enabled == false && OnOff == true)
                    {
                        audioSourceS.enabled = true;
                        SoundsCross.SetActive(false);
                    }
                    if (audioSourceS.enabled == true && OnOff == false)
                    {
                        audioSourceS.enabled = false;
                        SoundsCross.SetActive(true);
                    }
                }
            }
        }
    }


    public void OnSoundsButtonClicked()
    {
        if (OnOff == true)
        {
            OnOff = false;
        }
        else
        {
            OnOff = true;
        }
    }
}