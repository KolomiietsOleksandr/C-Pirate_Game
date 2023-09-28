using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestLogic : MonoBehaviour
{
    public Button QuestPhoto;
    public GameObject QuestPanel;
    public Button TakeButton;
    public Button SkipButton;
    public Button CollectButton;
    public TextMeshProUGUI QuestDescription;
    public TextMeshProUGUI ProgressDescription;

    [Header("Animation")]
    public float Up;
    public float Down;
    public float AnimTime;
    public float speed;

    private bool isPanelExpanded = false; // Прапорець для визначення стану панелі
    private bool isAnimationInProgress = false; // Прапорець для визначення стану анімації

    private Vector3 initialPanelPos; // Початкова позиція панелі
    private Vector3 initialPhotoPos; // Початкова позиція кнопки

    private void Start()
    {
        // Зберігання початкових позицій панелі та кнопки
        initialPanelPos = QuestPanel.transform.position;
        initialPhotoPos = QuestPhoto.transform.position;
    }

    public void OnQuestClicked()
    {
        // Перевірка, чи триває анімація
        if (isAnimationInProgress)
        {
            return; // Якщо анімація вже виконується, не робимо нічого
        }

        if (isPanelExpanded)
        {
            // Визначення кінцевої позиції для закриття панелі
            Vector3 targetPanelPos = initialPanelPos;
            Vector3 targetPhotoPos = initialPhotoPos;

            // Виклик корутини для закриття панелі
            StartCoroutine(MoveQuestPanel(targetPanelPos, targetPhotoPos));

            // Зміна стану панелі і стану анімації
            isPanelExpanded = false;
            isAnimationInProgress = true;

            // Заблокувати кнопку під час анімації
            QuestPhoto.interactable = false;
        }
        else
        {
            // Визначення кінцевої позиції для відкриття панелі
            Vector3 targetPanelPos = initialPanelPos + Vector3.down * Down;
            Vector3 targetPhotoPos = initialPhotoPos + Vector3.up * Up;

            // Виклик корутини для відкриття панелі
            StartCoroutine(MoveQuestPanel(targetPanelPos, targetPhotoPos));

            // Зміна стану панелі і стану анімації
            isPanelExpanded = true;
            isAnimationInProgress = true;

            // Заблокувати кнопку під час анімації
            QuestPhoto.interactable = false;
        }
    }

    private IEnumerator MoveQuestPanel(Vector3 targetPanelPos, Vector3 targetPhotoPos)
    {
        float journeyLength = Vector3.Distance(QuestPanel.transform.position, targetPanelPos);
        float startTime = Time.time;

        while (Time.time - startTime < AnimTime)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;

            // Застосування інтерполяції для зміщення панелі і кнопки
            QuestPanel.transform.position = Vector3.Lerp(QuestPanel.transform.position, targetPanelPos, fractionOfJourney);
            QuestPhoto.transform.position = Vector3.Lerp(QuestPhoto.transform.position, targetPhotoPos, fractionOfJourney);

            yield return null;
        }

        // Завершення анімації, встановлення точної позиції
        QuestPanel.transform.position = targetPanelPos;
        QuestPhoto.transform.position = targetPhotoPos;

        // Зміна стану анімації та розблокування кнопки
        QuestPhoto.interactable = true;
        isAnimationInProgress = false;
    }
}
