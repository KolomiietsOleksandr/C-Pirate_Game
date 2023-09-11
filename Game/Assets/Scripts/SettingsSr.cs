using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSr : MonoBehaviour
{
    public string[] Ui;
    private GameObject CurrentUi;

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
}