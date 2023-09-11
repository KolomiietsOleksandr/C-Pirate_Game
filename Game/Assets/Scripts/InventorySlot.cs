using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Sprite SpecSprite;
    public Sprite NormalSprite;
    public Color selectedColor, notSelectedColor;
    public bool FastItemSlot = false;
    public SlotType type;

    public bool IsSelected = false;

    public PlayerData playerData;
    
    private void Awake()
    {
        playerData = FindObjectOfType<PlayerData>();
    }
    
    public void Update()
    {
        if (type != SlotType.SimpleSlot || type != SlotType.FastSlot)
        {
            if (transform.childCount == 0)
            {
                image.sprite = SpecSprite;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        int residue;
        InventoryItem currentDragItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        InventoryItem itemInSlot = GetComponentInChildren<InventoryItem>();
        if (transform.childCount == 0)
        {
            if (type == SlotType.SimpleSlot || type == SlotType.FastSlot)
            {
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                inventoryItem.parentAfterDrag = transform;
                if (currentDragItem.item.type == ItemType.Weapon || currentDragItem.item.type == ItemType.Clothes || currentDragItem.item.type == ItemType.Artifact || currentDragItem.item.type == ItemType.Treasure)
                {
                    playerData.RemoveItemEffects(currentDragItem.item);
                    if (currentDragItem.item.type == ItemType.Clothes)
                    {
                        playerData.CurrentAnimator = playerData.animators[0];
                    }
                }
            }
            if (type == SlotType.SimpleSlot && currentDragItem.item.type != ItemType.Food && currentDragItem.item.type != ItemType.Health)
            {
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                inventoryItem.parentAfterDrag = transform;
            }
            if (type == SlotType.ClothesSlot && currentDragItem.item.type == ItemType.Clothes)
            {
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                inventoryItem.parentAfterDrag = transform;
                image.sprite = NormalSprite;

                
                if (currentDragItem.item.ItemName == "Sailor's uniform")
                {
                    playerData.ApplyItemEffects(currentDragItem.item);
                    playerData.CurrentAnimator = playerData.animators[1];
                }
                if (currentDragItem.item.ItemName == "Pirate costume")
                {
                    playerData.ApplyItemEffects(currentDragItem.item);
                    playerData.CurrentAnimator = playerData.animators[2];
                }
            }
            if (type == SlotType.WeaponSlot && currentDragItem.item.type == ItemType.Weapon)
            {
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                inventoryItem.parentAfterDrag = transform;
                image.sprite = NormalSprite;
                playerData.ApplyItemEffects(currentDragItem.item);
            }
            if (type == SlotType.ArtifactSlot && currentDragItem.item.type == ItemType.Artifact)
            {
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                inventoryItem.parentAfterDrag = transform;
                image.sprite = NormalSprite;
                playerData.ApplyItemEffects(currentDragItem.item);
            }
        }

        if (transform.childCount != 0)
        {
            if (currentDragItem.item.name == itemInSlot.item.name && itemInSlot.count <= currentDragItem.count || currentDragItem.item.name == itemInSlot.item.name && itemInSlot.count >= currentDragItem.count)
            {
                if (itemInSlot.count < itemInSlot.item.MaxStackValue)
                {
                    itemInSlot.count += currentDragItem.count;
                    residue = itemInSlot.count - itemInSlot.item.MaxStackValue;

                    if (itemInSlot.count > itemInSlot.item.MaxStackValue)
                    {
                        itemInSlot.count = itemInSlot.item.MaxStackValue;
                        currentDragItem.count = residue;
                    }
                    if (residue <= 0)
                    {
                        Destroy(eventData.pointerDrag);
                    }
                }
                currentDragItem.RefreshCount();
                itemInSlot.RefreshCount();
            }
        }
    }
}

public enum SlotType
{
    FastSlot,
    SimpleSlot,
    WeaponSlot,
    ClothesSlot,
    ArtifactSlot,
    LootSlot,
    SellSlot,
    BuySlot
}
