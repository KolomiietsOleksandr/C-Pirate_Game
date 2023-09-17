using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuySellButton : MonoBehaviour
{
    public InventoryItem inventoryItem;
    public InventorySlot inventorySlot;
    public GameObject Slot;
    public Item item;
    private GameObject BuySell;
    public InventoryDuplicator InventoryDuplicatorGo;

    public TextMeshProUGUI MerchantText;
    public TextMeshProUGUI ButtonText;
    public int MPPhase = 0;
    public string[] MerchantPhrases;


    public void BuyItem()
    {
        if (inventoryItem != null && item != null && inventorySlot.type == SlotType.BuySlot)
        {
            inventoryItem.BuyItem(item);

            // Вызываем DuplicateItems только после успешной продажи предмета
            InventoryDuplicatorGo.Invoke("DuplicateItems", 0.1f);
        }
    }


    public void SellItem()
    {
        if (inventoryItem != null && item != null && inventorySlot.type != SlotType.BuySlot)
        {
            inventoryItem.SellItem(item);

            // Вызываем DuplicateItems только после успешной продажи предмета
            InventoryDuplicatorGo.Invoke("DuplicateItems", 0.1f);
        }
    }


    public void Update()
    {
        if (Slot != null)
        {
            inventoryItem = Slot.GetComponentInChildren<InventoryItem>();
            inventorySlot = Slot.GetComponent<InventorySlot>();
        }

        if (Slot != null && inventorySlot.type != SlotType.BuySlot)
        {
            ButtonText.text = "Sell";
        }       
        if (Slot != null && inventorySlot.type == SlotType.BuySlot)
        {
            ButtonText.text = "Buy";
        }
        if (Slot == null)
        {
            ButtonText.text = "Choose";
        }

        if (inventoryItem == null) 
        {
            MPPhase = 0;
        }
        if (inventoryItem != null && inventorySlot.type != SlotType.BuySlot)
        {
            MerchantPhrases[1] = $"Are you sure you want to sell {inventoryItem.item.name} for {inventoryItem.item.ItemSell}?";
            MPPhase = 1;
        }
        if (inventoryItem != null && inventorySlot.type == SlotType.BuySlot)
        {
            MerchantPhrases[2] = $"Are you sure you want to buy {inventoryItem.item.name} for {inventoryItem.item.ItemBuy}?";
            MPPhase = 2;
        }

        switch (MPPhase)
        {
            case 0:
                MerchantText.text = MerchantPhrases[0];
                break;
            case 1:
                MerchantText.text = MerchantPhrases[1];
                break; 
            case 2:
                MerchantText.text = MerchantPhrases[2];
                break;
        }
    }


    public void ResetMPP()
    {
        MPPhase = 0;
        Slot = null;
        inventorySlot = null;
        inventoryItem = null;
    }
}
