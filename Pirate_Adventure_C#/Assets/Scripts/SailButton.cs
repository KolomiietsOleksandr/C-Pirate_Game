using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SailButton : MonoBehaviour
{
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private string[] PassivePhrases;
    [SerializeField] private string[] InteravtiveEvents;

    int timeH = 12;
    int timeM = 0;

    public void GetEvent()
    {   
        if (timeH < 24) { timeH = timeH + InSailTiimer.hours; }
        if (timeM > 24) { timeH = 00;}
        if (timeM < 60) { timeM = timeM + InSailTiimer.hours; }
        if (timeM > 60) { timeM = 00; }


        int EventsTypeRandomiser = Random.Range(0, 11);

        if (EventsTypeRandomiser >= 0 && EventsTypeRandomiser <= 4)
        {
            NameText.text += $"{timeH}:{timeM} | " + PassivePhrases[0] + '\n' + '\n';
        }

        else if (EventsTypeRandomiser == 5 || EventsTypeRandomiser == 6)
        {
            NameText.text += $"{timeH}:{timeM} | " + PassivePhrases[Random.Range(1, PassivePhrases.Length)] + '\n' + '\n';
        }

        else if (EventsTypeRandomiser == 7 || EventsTypeRandomiser == 8)
        {
            NameText.text += $"{timeH}:{timeM} | " + "Captain, on the course <color=red>enemy vessel</color>" + '\n' + '\n';
        }

        else if (EventsTypeRandomiser == 9) 
        {
            NameText.text += $"{timeH}:{timeM} | " + "Captain, on the course <color=yellow>merchant ship</color>" + '\n' + '\n';
        }

        else if (EventsTypeRandomiser == 10)
        {
            NameText.text += $"{timeH}:{timeM} | " + "<color=green>I see a bottle, maybe there's a map in it?</color>" + '\n' + '\n';
        }
    }


}
