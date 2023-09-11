using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantShip : MonoBehaviour
{
    public Item[] AllMerchantItems;
    public int NumberOfItemsToDisplay = 4; //  ≥льк≥сть предмет≥в, €к≥ потр≥бно в≥добразити
    public Item[] CurrentMerchantItems;

    private void Start()
    {
        GenerateMerchantItems();
    }

    public void GenerateMerchantItems()
    {
        List<int> availableIndices = new List<int>();

        for (int i = 0; i < AllMerchantItems.Length; i++)
        {
            availableIndices.Add(i);
        }

        CurrentMerchantItems = new Item[NumberOfItemsToDisplay];

        for (int i = 0; i < NumberOfItemsToDisplay; i++)
        {
            if (availableIndices.Count == 0)
            {
                break; // якщо не залишилос€ доступних предмет≥в, вийти з циклу
            }

            int randomIndex = Random.Range(0, availableIndices.Count);
            int selectedIndex = availableIndices[randomIndex];

            CurrentMerchantItems[i] = AllMerchantItems[selectedIndex];
            availableIndices.RemoveAt(randomIndex);
        }
    }
}
