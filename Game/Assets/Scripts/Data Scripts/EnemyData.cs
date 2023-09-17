using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyData : MonoBehaviour
{
    public int level;
    public int health;
    public int maxHealth;
    public int damage;
    public int armor;
    public int CriticalMultiplier;
    public int CriticalChance;

    public Item[] DropItems;

    public Slider HealthSlider;

    public bool MovingFromBack = false;

    public void FixedUpdate()
    {
        HealthSlider.minValue = 0;
        HealthSlider.maxValue = maxHealth;

        HealthSlider.value = health;
    }

    public void DestroyHealthBar()
    {
        Destroy(GameObject.FindGameObjectWithTag("EnHealthSlider"));
    }
}
