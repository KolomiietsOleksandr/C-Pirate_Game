using UnityEngine;

public class EndlessMovement : MonoBehaviour
{
    public float movementSpeed = 1f;    // �������� ���� ��'����
    public float boundaryOffset = 0.1f; // ³����� �� ��� ������

    private SpriteRenderer spriteRenderer;   // ��������� SpriteRenderer ��'����
    private bool isMovingLeft = true;         // ���������, �� �������, �� ��'��� �������� ����
    private float objectWidth;                // ������ ��'����

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectWidth = spriteRenderer.bounds.size.x;
    }

    private void Update()
    {
        // ������ ��'��� ���� ��� ������ � ����������� ��������
        float moveAmount = movementSpeed * Time.deltaTime;
        if (isMovingLeft)
        {
            transform.Translate(Vector2.left * moveAmount);
        }
        else
        {
            transform.Translate(Vector2.right * moveAmount);
        }

        // ����������, �� ��'��� ����� ��� ������ � ���� ����
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
        // �������� ������������ �������, ���� ��'��� ����� �������� ����
        if ((isMovingLeft && transform.localScale.x > 0) || (!isMovingLeft && transform.localScale.x < 0))
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        // ������� �������� ������� ��'����
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void ChangeDirection()
    {
        // �����, ���� ����� �������� ���� ��'����
        isMovingLeft = !isMovingLeft;
    }
}
