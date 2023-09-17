using UnityEngine;

public class ScreenShakeHit : MonoBehaviour
{
    public CameraShakeManager cameraShakeManager;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;

    private void Start()
    {
        // �����'����� ��������� �� CameraShakeManager (���������� �� "CameraShakeManager" ��'����)
        cameraShakeManager = GameObject.Find("Main Camera").GetComponent<CameraShakeManager>();
    }

    public void OnHit()
    {
        // ��������� ����������� ������
        cameraShakeManager.Shake(shakeDuration, shakeMagnitude);
    }
}
