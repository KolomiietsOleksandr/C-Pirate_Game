using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    [System.Serializable]
    public class ShopItemInfo
    {
        public Item item;
        public bool isSpawned;
    }

    public ShopItemInfo[] ShopItemsInfo;
    public InventorySlot[] BuySlots;
    public InventoryManager inventoryManager;
    public MerchantShip merchantShip;
    public HumanSprite humanSprite;

    public UIController uIController;

    private void Start()
    {
        UpdateMerchantShipReference();
        SpawnLootInShop();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Sail") 
        {
            UpdateMerchantShipReference();
        }
    }

    private void UpdateMerchantShipReference()
    {
        GameObject Ship = GameObject.FindWithTag("MerchantShip");
        if (Ship != null)
        {
            merchantShip = Ship.GetComponent<MerchantShip>();
        }
    }

    private void ClearBuySlots()
    {
        foreach (var slot in BuySlots)
        {
            if (slot.transform.childCount > 0)
            {
                Destroy(slot.transform.GetChild(0).gameObject);
            }
        }

        foreach (var itemInfo in ShopItemsInfo)
        {
            itemInfo.isSpawned = false;
        }
    }

    public void SpawnLootInShop()
    {
        ClearBuySlots();

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Pier")
        {
            uIController.InteractionsButtonsOn();
            humanSprite.ChangeSptite(0);
            Invoke("currentItemInfoFUN", 0.1f);
        }

        if (currentSceneName == "Sail")
        {
            uIController.InteractionsButtonsOFF();
            humanSprite.ChangeSptite(1);
            Invoke("CurrentMerchantItemsFUN", 0.1f);
        }
    }

    public void currentItemInfoFUN()
    {
        for (int i = 0; i < ShopItemsInfo.Length; i++)
        {
            ShopItemInfo currentItemInfo = ShopItemsInfo[i];
            if (!currentItemInfo.isSpawned)
            {
                Item currentItem = currentItemInfo.item;

                InventorySlot currentSlot = BuySlots[i];
                inventoryManager.SpawnNewItem(currentItem, 1, currentSlot);
                currentItemInfo.isSpawned = true;
            }
        }
    }

    public void CurrentMerchantItemsFUN()
    {
        for (int i = 0; i < merchantShip.CurrentMerchantItems.Length && i < BuySlots.Length; i++)
        {
            ShopItemInfo currentItemInfo = ShopItemsInfo[i];
            if (!currentItemInfo.isSpawned)
            {
                Item currentItem = merchantShip.CurrentMerchantItems[i];

                InventorySlot currentSlot = BuySlots[i];
                inventoryManager.SpawnNewItem(currentItem, 1, currentSlot);
                currentItemInfo.isSpawned = true;
            }
        }
    }

    public void Human()
    {
        humanSprite.ChangeSptite(0);
    }
}
