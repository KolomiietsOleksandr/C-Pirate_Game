using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDuplicator : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public InventorySlot[] sourceSlots;
    public InventorySlot[] destinationSlots;


    public void DuplicateItems()
    {
        ClearDestinationSlots();

        for (int i = 0; i < sourceSlots.Length; i++)
        {
            InventorySlot sourceSlot = sourceSlots[i];
            InventoryItem sourceItem = sourceSlot.GetComponentInChildren<InventoryItem>();

            if (sourceItem != null)
            {
                InventorySlot destinationSlot = destinationSlots[i];
                SpawnNewItem(sourceItem.item, sourceItem.count, destinationSlot);

                // Обновляем destinationSlots данными из sourceSlots
                InventoryItem destinationItem = destinationSlot.GetComponentInChildren<InventoryItem>();
                destinationItem.InitialiseItem(sourceItem.item, sourceItem.count);
                destinationItem.RefreshCount();
            }
        }
    }


    public void ClearDestinationSlots()
    {
        foreach (var slot in destinationSlots)
        {
            ClearDestinationSlot(slot);           
        }
    }


    public void ClearDestinationSlot(InventorySlot slot)
    {
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Destroy(itemInSlot.gameObject);
        }
    }


    public void SpawnNewItem(Item item, int count, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryManager.inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item, count);
        inventoryItem.RefreshCount();
    }
}