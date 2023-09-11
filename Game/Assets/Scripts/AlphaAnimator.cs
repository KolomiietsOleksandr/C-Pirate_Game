using UnityEngine;

public class AlphaAnimator : MonoBehaviour
{
    public float minAlpha = 0f; // ̳������� �������� �����-������
    public float maxAlpha = 1f; // ����������� �������� �����-������
    public float animationDuration = 2f; // ��������� ������� � ��������

    private float currentAlpha; // ������� �������� �����-������
    private float timer; // ������ ��� �������

    private bool increasing; // ����, �� ������, �� �������� �����-�����
    private bool isAnimating; // ����, �� ������, �� ����� �������

    private void Start()
    {
        currentAlpha = minAlpha; // ������������ ��������� �������� �����-������ (���������)
        timer = 0f; // ������� ������
        increasing = true; // �������� ���������� �����-�����
        isAnimating = false; // �������, �� ������� �� �����

        ApplyAlpha(); // ����������� �������� �����-������ ��� �����
    }

    private void Update()
    {
        if (isAnimating)
        {
            // �������� ������ �� ���, ��������� � ���������� �����
            timer += Time.deltaTime;

            if (increasing)
            {
                // ���������� ���� �������� �����-������ � ������������� ������ ��������
                currentAlpha = Mathf.Lerp(minAlpha, maxAlpha, timer / animationDuration);

                // ����������, �� ��������� ������������� �������� �����-������
                if (currentAlpha >= maxAlpha)
                {
                    // ��������� ��������� �����-������
                    increasing = false;
                    timer = 0f;
                }
            }
            else
            {
                // ���������� ���� �������� �����-������ � ������������� ������ ��������
                currentAlpha = Mathf.Lerp(maxAlpha, minAlpha, timer / animationDuration);

                // ����������, �� ��������� ���������� �������� �����-������
                if (currentAlpha <= minAlpha)
                {
                    // ��������� ��������� �����-������
                    isAnimating = false; // ��������� �������
                    currentAlpha = minAlpha; // ������������ �����-����� � �������� ��������
                }
            }

            ApplyAlpha(); // ����������� ���� �������� �����-������ �� ��'����
        }
    }

    public void PlayAnimation()
    {
        // �������� �������, ���� ���� �� �����
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
        // �������� ���������� ���� ��'����
        Color objectColor = GetComponent<Renderer>().material.color;

        // ������� �������� �����-������
        objectColor.a = currentAlpha;

        // ����������� ����� ���� �� ��'����
        GetComponent<Renderer>().material.color = objectColor;
    }
}
