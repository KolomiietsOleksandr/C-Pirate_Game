using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sptites;

    public void ChangeSptite(int a)
    {
        spriteRenderer.sprite = sptites[a];
    }
}
