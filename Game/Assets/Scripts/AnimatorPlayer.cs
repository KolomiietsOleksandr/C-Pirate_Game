using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayer : MonoBehaviour
{
    public Animator animatorPl;
    public PlayerData playerData;

    public void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    public void Update()
    {
        animatorPl.runtimeAnimatorController = playerData.CurrentAnimator;
    }
}
