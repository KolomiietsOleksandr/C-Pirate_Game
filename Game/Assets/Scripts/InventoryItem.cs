using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public Item item;
    public TextMeshProUGUI countText;

    [HideInInspector] public Transform parentAfterDrag;
    public Image image;
    [HideInInspector] public int count = 1;
    private PlayerData playerData;

    public InventorySlot currentSlot;

    private float doubleClickTimeThreshold = 0.3f; // Поріг для визначення подвійного натискання
    public bool isClickPending = false; // Флаг, що вказує, що очікується друге натискання
    private float lastClickTime = 0f; // Час останнього натискання

    private LootButtonController lootButtonController; // Додано змінну для посилання на контролер кнопок
    private GameObject TakeAllButton;
    private GameObject TakeButton;

    public GameObject BuySellButton;

    private int slotIndex = -1;


    private void Awake()
    {

        playerData = FindObjectOfType<PlayerData>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void InitialiseItem(Item newItem, int itemCount)
    {
        item = newItem;
        count = itemCount;
        image.sprite = newItem.image;
        RefreshCount();
    }


    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool TextActive = count > 1;
        countText.gameObject.SetActive(TextActive);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }


    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.dragging)
            return;

        currentSlot = GetComponentInParent<InventorySlot>(); // Отримання посилання на поточний слот

        InventoryItem currentItem = eventData.pointerEnter.GetComponent<InventoryItem>();

        if (currentItem != null)
        {

            if (isClickPending && Time.time - lastClickTime < doubleClickTimeThreshold && currentSlot.type == SlotType.SimpleSlot || isClickPending && Time.time - lastClickTime < doubleClickTimeThreshold && currentSlot.type == SlotType.FastSlot)
            {
                // Виконується подвійне натискання
                isClickPending = false;
                
                if (SceneManager.GetActiveScene().name == "Battle" || SceneManager.GetActiveScene().name == "Map")
                {
                    GameObject Manager = GameObject.FindGameObjectWithTag("GameController");
                    HumanFight HumanFightGO = Manager.GetComponent<HumanFight>();
                    if (HumanFightGO.IsMove == 0 || HumanFightGO.IsMove == 4)  
                    {
                        Debug.Log(HumanFightGO.IsMove);
                        ItemAction(currentItem.item);
                        HumanFightGO.Invoke("SkipPlayerStep", 0.1f);
                    }
                }
                if (SceneManager.GetActiveScene().name != "Battle" && SceneManager.GetActiveScene().name != "Map")
                {
                    ItemAction(currentItem.item);
                }
            }
            else
            {
                // Виконується одинарне натискання
                isClickPending = true;
                lastClickTime = Time.time;
                if (currentSlot.type == SlotType.SimpleSlot)
                {
                    GameObject itemDescriptionGO = GameObject.FindGameObjectWithTag("ItemDescription");
                    TextMeshProUGUI ItemDescription = itemDescriptionGO.GetComponent<TextMeshProUGUI>();
                    ItemDescription.text = currentItem.item.description;
                }
                if (currentSlot.type == SlotType.LootSlot)
                {
                    GameObject lootDisplay = GameObject.FindGameObjectWithTag("LootDisplay");
                    if (lootDisplay != null)
                    {
                        Transform childTransform1 = lootDisplay.transform.Find("TakeAllLootButton");
                        TakeAllButton = childTransform1.gameObject;
                        Transform childTransform2 = lootDisplay.transform.Find("TakeLootButton");
                        TakeButton = childTransform2.gameObject;
                    }

                    TakeAllButton.SetActive(false);
                    TakeButton.SetActive(true);

                    lootButtonController = FindObjectOfType<LootButtonController>(); // Знайдіть інстанцію контролера кнопок
                    // Оновіть вибраний предмет у контролері кнопок
                    if (lootButtonController != null)
                    {
                        lootButtonController.SetSelectedItem(this);
                    }
                }
                if (currentSlot.type == SlotType.SellSlot)
                {
                    GameObject InteractionsGo = GameObject.Find("Sell/Buy");
                    InventoryDuplicator InventoryDuplicatorGo = InteractionsGo.GetComponent<InventoryDuplicator>();
                    InventorySlot[] sourceSlotsGo = InventoryDuplicatorGo.sourceSlots;
                    InventorySlot[] destinationSlotsGo = InventoryDuplicatorGo.destinationSlots;

                    if (sourceSlotsGo != null && destinationSlotsGo != null)
                    {

                        GameObject currentSlotDes = currentItem.transform.parent.gameObject;

                        // Find the selected slot index
                        for (int i = 0; i < destinationSlotsGo.Length; i++)
                        {
                            if (destinationSlotsGo[i].name == currentSlotDes.name)
                            {
                                slotIndex = i;
                                break;
                            }
                        }

                        // Deselect other slots and select this one
                        for (int i = 0; i < destinationSlotsGo.Length; i++)
                        {
                            destinationSlotsGo[i].IsSelected = false;
                        }
                        SelectSlotAndItem(currentItem.item);
                    }
                }
                if (currentSlot.type == SlotType.BuySlot)
                {
                    GameObject currentSlotDes = currentItem.transform.parent.gameObject;

                    BuySellButton = GameObject.Find("Buy/Sell Button");
                    BuySellButton BuySellButtonGo = BuySellButton.GetComponent<BuySellButton>();

                    BuySellButtonGo.Slot = currentSlotDes;
                    BuySellButtonGo.item = item;
                }
            }
        }
    }


    public void SelectSlotAndItem(Item item)
    {
        GameObject InteractionsGo = GameObject.Find("Sell/Buy");
        InventoryDuplicator InventoryDuplicatorGo = InteractionsGo.GetComponent<InventoryDuplicator>();
        InventorySlot[] sourceSlotsGo = InventoryDuplicatorGo.sourceSlots;
        InventorySlot[] destinationSlotsGo = InventoryDuplicatorGo.destinationSlots;

        if (slotIndex >= 0 && slotIndex < sourceSlotsGo.Length)
        {
            BuySellButton = GameObject.Find("Buy/Sell Button");
            BuySellButton BuySellButtonGo = BuySellButton.GetComponent<BuySellButton>();

            BuySellButtonGo.Slot = sourceSlotsGo[slotIndex].gameObject;
            BuySellButtonGo.item = item;   
        }
    }

    public void BuyItem(Item item)
    {
        if (playerData.money >= item.ItemBuy)
        {
            playerData.money -= item.ItemBuy;
            InventoryManager inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();

            // Перевірка наявності слота без такого ж предмета
            for (int i = 0; i < inventoryManager.inventorySlots.Length; i++)
            {
                InventorySlot slot = inventoryManager.inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                if (itemInSlot == null && slot.type == SlotType.SimpleSlot)
                {
                    inventoryManager.SpawnNewItem(item, 1, slot);
                    

                    BuySellButton = GameObject.Find("Buy/Sell Button");
                    BuySellButton BuySellButtonGo = BuySellButton.GetComponent<BuySellButton>();

                    BuySellButtonGo.ResetMPP();
                    return; // Вихід з функції після додавання предмета
                }
            }
        }
    }


    public void SellItem(Item item)
    {
        //GameObject Invetory = GameObject.Find("Inventory");
        //Invetory.SetActive(true);
        //Invetory.SetActive(false);
        playerData = FindObjectOfType<PlayerData>();
        playerData.money += item.ItemSell;
        count--;
        RefreshCount();
        if (count <= 0)
        {
            Destroy(gameObject);
            BuySellButton = GameObject.Find("Buy/Sell Button");
            BuySellButton BuySellButtonGo = BuySellButton.GetComponent<BuySellButton>();

            BuySellButtonGo.ResetMPP();
        }
    }


    public void ItemAction(Item item)
    {
        switch (item.type.ToString())
        {
            case "Food":
                playerData.Hunger += item.HungerUp;
                playerData.Thirst += item.ThirstUp;
                playerData.energy += item.EnergyUp;
                count--;
                RefreshCount();
                if (count <= 0)
                {
                    Destroy(gameObject);
                }
                break;
            case "Drink":
                playerData.Thirst += item.ThirstUp;
                playerData.energy += item.EnergyUp;
                count--;
                RefreshCount();
                if (count <= 0)
                {
                    Destroy(gameObject);
                }
                break;
            case "Health":
                playerData.health += item.HealthUp;
                count--;
                RefreshCount();
                if (count <= 0)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}