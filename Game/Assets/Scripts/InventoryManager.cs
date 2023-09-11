using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public bool AddItemLoot(Item item, int count)
    {
        GameObject lootDisplay = GameObject.FindGameObjectWithTag("LootDisplay");

        if (lootDisplay != null)
        {
            InventorySlot[] lootSlots = lootDisplay.GetComponentsInChildren<InventorySlot>();

            for (int i = 0; i < lootSlots.Length; i++)
            {
                InventorySlot slot = lootSlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                if (itemInSlot == null && slot.FastItemSlot != true && slot.type != SlotType.SimpleSlot && slot.type == SlotType.LootSlot)
                {
                    SpawnNewItem(item, count, slot);
                    return true;
                }
            }
        }
        return false;
    }


    public bool AddItem(Item item, int count)
    {
        /* Додає не враховвуючи макс.  стаці, не добавляє інший предмет, знаючи остачу
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < itemInSlot.item.MaxStackValue && itemInSlot.item.stackable == true && slot.FastItemSlot != true && slot.type == SlotType.SimpleSlot)
            {
                itemInSlot.count += item.count;
                itemInSlot.RefreshCount();
                return true;
            }
        }*/
        

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null && slot.FastItemSlot != true && slot.type == SlotType.SimpleSlot)
            {
                SpawnNewItem(item, count, slot);
                return true;
            }
        }
        return false;
    }

    public void SpawnNewItem(Item item, int count, InventorySlot slot)
    {
        if (inventoryItemPrefab  != null) 
        { 
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item, count);
        inventoryItem.RefreshCount();
        }     
    }

    public void DeleteInvDie()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            if (slot.transform.childCount > 0 && slot.type == SlotType.FastSlot || slot.transform.childCount > 0 && slot.type == SlotType.SimpleSlot)
            {   
                GameObject itemInSlot = slot.transform.GetChild(0).gameObject;
                Destroy(itemInSlot);
            }
        }
    }
}