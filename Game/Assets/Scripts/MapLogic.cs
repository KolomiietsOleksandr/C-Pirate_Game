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
        int numTreasureCrosses = 3; // ʳ������ ������, �� ������� ������� �� ������
        List<Cross> availableCrosses = new List<Cross>(crosses); // ��������� ������ ��������� ������

        for (int i = 0; i < numTreasureCrosses; i++)
        {
            if (availableCrosses.Count == 0)
                break; // ����� � �����, ���� ����� ���� ��������� ������

            int randomIndex = Random.Range(0, availableCrosses.Count); // ���� ����������� �������
            Cross randomCross = availableCrosses[randomIndex]; // ��������� ����������� ������

            randomCross.TreasureCross = true; // ������������ �������� TreasureCross �� true

            availableCrosses.RemoveAt(randomIndex); // ��������� ��������� ������ � ������ ��������� ������
        }
    }
}
