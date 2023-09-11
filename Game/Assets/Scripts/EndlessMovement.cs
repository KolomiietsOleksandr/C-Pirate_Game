using UnityEngine;

public class EndlessMovement : MonoBehaviour
{
    public float movementSpeed = 1f;    // Швидкість руху об'єкту
    public float boundaryOffset = 0.1f; // Відступ від межі екрану

    private SpriteRenderer spriteRenderer;   // Компонент SpriteRenderer об'єкту
    private bool isMovingLeft = true;         // Прапорець, що визначає, чи об'єкт рухається вліво
    private float objectWidth;                // Ширина об'єкту

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectWidth = spriteRenderer.bounds.size.x;
    }

    private void Update()
    {
        // Рухаємо об'єкт вліво або вправо з врахуванням швидкості
        float moveAmount = movementSpeed * Time.deltaTime;
        if (isMovingLeft)
        {
            transform.Translate(Vector2.left * moveAmount);
        }
        else
        {
            transform.Translate(Vector2.right * moveAmount);
        }

        // Перевіряємо, чи об'єкт досяг межі екрану з обох боків
        if (isMovingLeft && transform.position.x + objectWidth < Camera.main.ScreenToWorldPoint(Vector3.zero).x - boundaryOffset)
        {
            ChangeDirection();
        }
        else if (!isMovingLeft && transform.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + boundaryOffset)
        {
            ChangeDirection();
        }
    }

    private void LateUpdate()
    {
        // Виконуємо перевернення спрайту, якщо об'єкт змінює напрямок руху
        if ((isMovingLeft && transform.localScale.x > 0) || (!isMovingLeft && transform.localScale.x < 0))
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        // Змінюємо напрямок спрайту об'єкту
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void ChangeDirection()
    {
        // Метод, який змінює напрямок руху об'єкту
        isMovingLeft = !isMovingLeft;
    }
}
