using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VesselsShop : MonoBehaviour
{
    public HumanSprite humanSprite;
    public PlayerData playerData;
    public VesselsData vesselsData;

    public TextMeshProUGUI DescriptionText;
    public Button NextButton; 
    public Button BackButton;

    public Sprite[] SpritesVessels; // Масив спрайтів
    public float[] x;
    public float[] y;
    public int currentSpriteIndex = 0; // Поточний індекс спрайта
    public GameObject SpriteRendererGO;

    public GameObject SelectedVessel;
    public GameObject[] PrefabVessel;
    public List<GameObject> PurchasedVessels;

    public Button BuyChooseButton;
    public string[] ButtonText;
    public string VesselDescrptionText;

    public void Start()
    {
        ChangeSprite();
    }

    public void Update()
    {
        if (currentSpriteIndex == 0)
        {
            BackButton.gameObject.SetActive(false);
        }

        if (currentSpriteIndex == SpritesVessels.Length - 1) 
        {
            NextButton.gameObject.SetActive(false);
        }

        if (currentSpriteIndex != 0)
        {
            BackButton.gameObject.SetActive(true);
        } 

        if (currentSpriteIndex != SpritesVessels.Length - 1)
        {
            NextButton.gameObject.SetActive(true);
        }


        if (PrefabVessel[currentSpriteIndex] != SelectedVessel && !PurchasedVessels.Contains(PrefabVessel[currentSpriteIndex]))
        {
            TextMeshProUGUI ButtonText = BuyChooseButton.GetComponentInChildren<TextMeshProUGUI>();
            ButtonText.text = "Buy";
        }
        if (PrefabVessel[currentSpriteIndex] != SelectedVessel && PurchasedVessels.Contains(PrefabVessel[currentSpriteIndex]))
        {
            TextMeshProUGUI ButtonText = BuyChooseButton.GetComponentInChildren<TextMeshProUGUI>();
            ButtonText.text = "Select";
        }
        if (PrefabVessel[currentSpriteIndex] == SelectedVessel)
        {
            TextMeshProUGUI ButtonText = BuyChooseButton.GetComponentInChildren<TextMeshProUGUI>();
            ButtonText.text = "Selected";
        }         
    }

    public void ClickNext()
    {
        if (currentSpriteIndex != SpritesVessels.Length - 1)
        {
            currentSpriteIndex += 1;
            ChangeSprite();
            VessselDescription();
        }
    }    
    public void ClickBack()
    {
        if (currentSpriteIndex != 0)
        {
            currentSpriteIndex -= 1;
            ChangeSprite();
            VessselDescription();
        }
    }

    public void ClearDescription()
    {
        DescriptionText.text = "";
    }

    public void Human()
    {
        humanSprite.ChangeSptite(3);
    }

    public void ChangeSprite()
    {
        SpriteRenderer spriteRenderer = SpriteRendererGO.GetComponent<SpriteRenderer>();
        RectTransform rectTransform = SpriteRendererGO.GetComponent<RectTransform>();

        spriteRenderer.sprite = SpritesVessels[currentSpriteIndex];

        Vector3 newScale = rectTransform.localScale; // Отримуємо поточний масштаб

        newScale.x = x[currentSpriteIndex];
        newScale.y = y[currentSpriteIndex];

        rectTransform.localScale = newScale; // Встановлюємо новий масштаб
    }

    public void BuyVesssel()
    {
        vesselsData = PrefabVessel[currentSpriteIndex].GetComponent<VesselsData>();
        if (playerData.money >= vesselsData.Cost) 
        {
            if (!PurchasedVessels.Contains(PrefabVessel[currentSpriteIndex]))
            {           
                playerData.money -= vesselsData.Cost;
                PurchasedVessels.Add(PrefabVessel[currentSpriteIndex]);
                VessselDescription();
            }
        }
    }


    public void Choose()
    {
        if (PurchasedVessels.Contains(PrefabVessel[currentSpriteIndex]))
        {
            SelectedVessel = PrefabVessel[currentSpriteIndex];
            playerData.currentShipPrefab = SelectedVessel;
            VessselDescription();
        }
    }

    public void VessselDescription()
    {
        if (PurchasedVessels.Contains(PrefabVessel[currentSpriteIndex]))
        {
            DescriptionText.text = $"Very nice vessel, captain, would you like to choose it for your next trip?";
        }

        if (!PurchasedVessels.Contains(PrefabVessel[currentSpriteIndex]))
        {
            vesselsData = PrefabVessel[currentSpriteIndex].GetComponent<VesselsData>();
            DescriptionText.text = $"Want to buy a new vessel for {vesselsData.Cost}?";
        }

        if (PrefabVessel[currentSpriteIndex] == SelectedVessel)
        {
            DescriptionText.text = "I'd like to sail the seas on such a vessel myself in search of adventure!";
        }
    }
}