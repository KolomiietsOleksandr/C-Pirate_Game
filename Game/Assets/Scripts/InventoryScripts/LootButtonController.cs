using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootButtonController : MonoBehaviour
{
    private GameObject inventoryManagerObj;
    private InventoryItem selectedItem; // Додано змінну для відстеження вибраного предмета
    private GameObject TakeAllButton;
    private GameObject TakeButton;

    public void OnTakeAllButtonClicked()
    {
        GameObject lootDisplay = GameObject.FindGameObjectWithTag("LootDisplay");
        inventoryManagerObj = GameObject.FindGameObjectWithTag("InventoryManager");

        if (lootDisplay != null)
        {
            InventoryManager inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
            InventorySlot[] lootSlots = lootDisplay.GetComponentsInChildren<InventorySlot>();

            foreach (InventorySlot slot in lootSlots)
            {
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                if (itemInSlot != null)
                {
                    bool result = inventoryManager.AddItem(itemInSlot.item, itemInSlot.count);
                    if (result)
                    {
                        Destroy(itemInSlot.gameObject);
                    }
                }
            }
        }
    }

    public void OnTakeButtonClicked()
    {
        if (selectedItem != null)
        {
            GameObject lootDisplay = GameObject.FindGameObjectWithTag("LootDisplay");
            inventoryManagerObj = GameObject.FindGameObjectWithTag("InventoryManager");

            if (lootDisplay != null)
            {
                InventoryManager inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
                bool result = inventoryManager.AddItem(selectedItem.item, selectedItem.count);

                if (result)
                {
                    Destroy(selectedItem.gameObject);
                    selectedItem = null; // Знуляємо вибраний предмет після переміщення в інвентар

                    if (selectedItem == null)
                    {
                        Transform childTransform1 = lootDisplay.transform.Find("TakeAllLootButton");
                        TakeAllButton = childTransform1.gameObject;
                        Transform childTransform2 = lootDisplay.transform.Find("TakeLootButton");
                        TakeButton = childTransform2.gameObject;
                    }
                    TakeAllButton.SetActive(true);
                    TakeButton.SetActive(false);
                }
            }
        }
    }

    public void SetSelectedItem(InventoryItem item)
    {
        selectedItem = item;
    }
}
