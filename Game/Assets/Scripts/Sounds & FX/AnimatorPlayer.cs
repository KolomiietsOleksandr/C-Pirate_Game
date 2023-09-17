using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayer : MonoBehaviour
{
    public Animator animatorPl;
    public PlayerData playerData;
    public HumanFight humanFight;
    public GameObject EventData;

    public void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    public void Update()
    {
        animatorPl.runtimeAnimatorController = playerData.CurrentAnimator;
        EventData = GameObject.Find("EventSystem");
        if (EventData != null )
        {
            humanFight = EventData.GetComponent<HumanFight>();
        }
    }

    public void isMove(int a)
    {
        humanFight.IsMoveChange(a);
    }
}
