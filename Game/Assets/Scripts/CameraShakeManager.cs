using System.Collections;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        // �����'����� ��������� ������� �� ��������� ������
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // ���������� ���������� ���� ������� �� ���������
            Vector3 shakePosition = originalPosition + Random.insideUnitSphere * magnitude;
            Quaternion shakeRotation = Quaternion.Euler(
                originalRotation.eulerAngles.x + Random.Range(-magnitude, magnitude),
                originalRotation.eulerAngles.y + Random.Range(-magnitude, magnitude),
                originalRotation.eulerAngles.z + Random.Range(-magnitude, magnitude));

            transform.position = shakePosition;
            transform.rotation = shakeRotation;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // ��������� ������ �� ��������� ������� �� ���������
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
