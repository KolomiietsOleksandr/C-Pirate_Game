using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public ItemType type;
    public int ItemBuy;
    public int ItemSell;    
    public int MaxStackValue;

    [Header("Only UI")]
    public bool stackable = true;
    public Sprite image;
    public string ItemName;
    [TextArea]
    public string description;

    [Header("Special Values")]
    [Header("Weapon Values")]
    public int AttackUp;
    public int CrtUp;
    public int EnFAtUPDOWN;

    [Header("Clothes Values")] 
    public int HealthUp;
    public int EnergyUp;
    public int ArmorUp;

    [Header("Artifact Values")] 
    public int LuckUP;

    [Header("Food/Drink Values")]
    public int HungerUp;
    public int ThirstUp;
}

public enum ItemType
{
    Health,
    Food,
    Weapon,
    Clothes,
    Artifact,
    Treasure
}