using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button SailButton;
    public Button DockButton;
    public Button InventoryButton;
    public Button EscapeButton;
    public Button FightButton;
    public Button BoardButton;
    public Button ShopButton;
    public Button SearchButton;
    public Button DefendButton;

    public Button[] InteractionsButtons;


    public void Start()
    {
        StreetUI();
    }


    public void SailUI()
    {
        SailButton.gameObject.SetActive(true);
        DockButton.gameObject.SetActive(true);
        InventoryButton.gameObject.SetActive(true);
        EscapeButton.gameObject.SetActive(false);
        FightButton.gameObject.SetActive(false);
        BoardButton.gameObject.SetActive(false);
        ShopButton.gameObject.SetActive(false);
        SearchButton.gameObject.SetActive(false);
        DefendButton.gameObject.SetActive(false);
    }


    public void PreFightUI()
    {
        SailButton.gameObject.SetActive(false);
        DockButton.gameObject.SetActive(false);
        InventoryButton.gameObject.SetActive(true);
        EscapeButton.gameObject.SetActive(true);
        FightButton.gameObject.SetActive(false);
        BoardButton.gameObject.SetActive(true);
        ShopButton.gameObject.SetActive(false);
        SearchButton.gameObject.SetActive(false);
        DefendButton.gameObject.SetActive(false);
    }


    public void FightUI()
    {
        SailButton.gameObject.SetActive(false);
        DockButton.gameObject.SetActive(false);
        InventoryButton.gameObject.SetActive(false);
        EscapeButton.gameObject.SetActive(true);
        FightButton.gameObject.SetActive(true);
        BoardButton.gameObject.SetActive(false);
        ShopButton.gameObject.SetActive(false);
        SearchButton.gameObject.SetActive(false);
        DefendButton.gameObject.SetActive(true);
    }


    public void MerchantVesselUI()
    {
        SailButton.gameObject.SetActive(true);
        DockButton.gameObject.SetActive(false);
        InventoryButton.gameObject.SetActive(true);
        EscapeButton.gameObject.SetActive(false);
        FightButton.gameObject.SetActive(false);
        BoardButton.gameObject.SetActive(false);
        ShopButton.gameObject.SetActive(true);
        SearchButton.gameObject.SetActive(false);
        DefendButton.gameObject.SetActive(false);
    }


    public void BottleMapUI()
    {
        SailButton.gameObject.SetActive(true);
        DockButton.gameObject.SetActive(false);
        InventoryButton.gameObject.SetActive(true);
        EscapeButton.gameObject.SetActive(false);
        FightButton.gameObject.SetActive(false);
        BoardButton.gameObject.SetActive(false);
        ShopButton.gameObject.SetActive(false);
        SearchButton.gameObject.SetActive(true);
        DefendButton.gameObject.SetActive(false);
    }

    public void StreetUI()
    {
        SailButton.gameObject.SetActive(true);
        DockButton.gameObject.SetActive(false);
        InventoryButton.gameObject.SetActive(true);
        EscapeButton.gameObject.SetActive(false);
        FightButton.gameObject.SetActive(false);
        BoardButton.gameObject.SetActive(false);
        ShopButton.gameObject.SetActive(true);
        SearchButton.gameObject.SetActive(false);
        DefendButton.gameObject.SetActive(false);
    }

    public void AttackInteractableFalse()
    {
        DefendButton.interactable = false;
        FightButton.interactable = false;
        EscapeButton.interactable = false;
    }

    public void AttackInteractableTrue()
    {
        DefendButton.interactable = true;
        FightButton.interactable = true;
        EscapeButton.interactable = true;
    }

    public void AfterFightUI()
    {
        SailButton.gameObject.SetActive(true);
        DockButton.gameObject.SetActive(false);
        InventoryButton.gameObject.SetActive(true);
        EscapeButton.gameObject.SetActive(false);
        FightButton.gameObject.SetActive(false);
        BoardButton.gameObject.SetActive(false);
        ShopButton.gameObject.SetActive(false);
        SearchButton.gameObject.SetActive(true);
        DefendButton.gameObject.SetActive(false);
    }

    public void RefreshUI()
    {
        SailButton.gameObject.SetActive(false);
        DockButton.gameObject.SetActive(false);
        InventoryButton.gameObject.SetActive(false);
        EscapeButton.gameObject.SetActive(false);
        FightButton.gameObject.SetActive(false);
        BoardButton.gameObject.SetActive(false);
        ShopButton.gameObject.SetActive(false);
        SearchButton.gameObject.SetActive(false);
        DefendButton.gameObject.SetActive(false);
    }

    public void InteractionsButtonsOn()
    {
        foreach (Button button in InteractionsButtons)
        {
            button.gameObject.SetActive(true);
        }
    }

    public void InteractionsButtonsOFF()
    {
        foreach (Button button in InteractionsButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}