using UnityEngine;

public class AlphaAnimator : MonoBehaviour
{
    public float minAlpha = 0f; // Мінімальне значення альфа-каналу
    public float maxAlpha = 1f; // Максимальне значення альфа-каналу
    public float animationDuration = 2f; // Тривалість анімації в секундах

    private float currentAlpha; // Поточне значення альфа-каналу
    private float timer; // Таймер для анімації

    private bool increasing; // Флаг, що показує, чи збільшуємо альфа-канал
    private bool isAnimating; // Флаг, що показує, чи триває анімація

    private void Start()
    {
        currentAlpha = minAlpha; // Встановлюємо початкове значення альфа-каналу (невидимий)
        timer = 0f; // Скидаємо таймер
        increasing = true; // Починаємо збільшувати альфа-канал
        isAnimating = false; // Вказуємо, що анімація не триває

        ApplyAlpha(); // Застосовуємо значення альфа-каналу при старті
    }

    private void Update()
    {
        if (isAnimating)
        {
            // Збільшуємо таймер на час, пройдений з останнього кадру
            timer += Time.deltaTime;

            if (increasing)
            {
                // Обчислюємо нове значення альфа-каналу з використанням ефекту плавності
                currentAlpha = Mathf.Lerp(minAlpha, maxAlpha, timer / animationDuration);

                // Перевіряємо, чи досягнуто максимального значення альфа-каналу
                if (currentAlpha >= maxAlpha)
                {
                    // Зупиняємо збільшення альфа-каналу
                    increasing = false;
                    timer = 0f;
                }
            }
            else
            {
                // Обчислюємо нове значення альфа-каналу з використанням ефекту плавності
                currentAlpha = Mathf.Lerp(maxAlpha, minAlpha, timer / animationDuration);

                // Перевіряємо, чи досягнуто мінімального значення альфа-каналу
                if (currentAlpha <= minAlpha)
                {
                    // Зупиняємо зменшення альфа-каналу
                    isAnimating = false; // Завершуємо анімацію
                    currentAlpha = minAlpha; // Встановлюємо альфа-канал в мінімальне значення
                }
            }

            ApplyAlpha(); // Застосовуємо нове значення альфа-каналу до об'єкта
        }
    }

    public void PlayAnimation()
    {
        // Починаємо анімацію, якщо вона не триває
        if (!isAnimating)
        {
            isAnimating = true;
            currentAlpha = minAlpha;
            timer = 0f;
            increasing = true;
        }
    }

    private void ApplyAlpha()
    {
        // Отримуємо початковий колір об'єкта
        Color objectColor = GetComponent<Renderer>().material.color;

        // Змінюємо значення альфа-каналу
        objectColor.a = currentAlpha;

        // Застосовуємо новий колір до об'єкта
        GetComponent<Renderer>().material.color = objectColor;
    }
}
