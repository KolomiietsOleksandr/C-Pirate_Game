using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MapLogic : MonoBehaviour, IPointerClickHandler
{
    public Cross[] crosses;

    private PlayerData playerData;
    private GameLogic gameLogic;
    public TextMeshProUGUI AttemptsText;
    private int Attempts;

    public void Awake()
    {
        playerData = FindObjectOfType<PlayerData>();
        gameLogic = FindObjectOfType<GameLogic>();
        Attempts = playerData.MaxAttempts;
        crosses = GetComponentsInChildren<Cross>();
        RandomTreaureCrosses();
    }

    public void Update()
    {
        AttemptsText.text = $"Attempts remain: {Attempts}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Attempts > 0)
        {
            Cross currentCross = eventData.pointerCurrentRaycast.gameObject.GetComponent<Cross>();
            if (currentCross != null)
            {
                if (currentCross.TreasureCross == true)
                {
                    currentCross.image.sprite = currentCross.GreenCross;
                    StartCoroutine(currentCross.BlinkAndTriggerEvent());
                    gameLogic.PreFight = 1;
                }

                if (currentCross.TreasureCross == false)
                {
                    Attempts--;
                    StartCoroutine(currentCross.FadeOutAndDestroy());
                }
            }
        }
    }

    public void RandomTreaureCrosses()
    {
        int numTreasureCrosses = 3; // Кількість крестів, які потрібно вибрати як скарби
        List<Cross> availableCrosses = new List<Cross>(crosses); // Створення списку доступних крестів

        for (int i = 0; i < numTreasureCrosses; i++)
        {
            if (availableCrosses.Count == 0)
                break; // Вихід з циклу, якщо більше немає доступних крестів

            int randomIndex = Random.Range(0, availableCrosses.Count); // Вибір випадкового індексу
            Cross randomCross = availableCrosses[randomIndex]; // Отримання випадкового креста

            randomCross.TreasureCross = true; // Встановлення значення TreasureCross на true

            availableCrosses.RemoveAt(randomIndex); // Видалення вибраного креста зі списку доступних крестів
        }
    }
}
