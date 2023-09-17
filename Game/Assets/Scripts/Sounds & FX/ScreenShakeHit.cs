using UnityEngine;

public class ScreenShakeHit : MonoBehaviour
{
    public CameraShakeManager cameraShakeManager;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;

    private void Start()
    {
        // Запам'ятати посилання на CameraShakeManager (прикріплене до "CameraShakeManager" об'єкта)
        cameraShakeManager = GameObject.Find("Main Camera").GetComponent<CameraShakeManager>();
    }

    public void OnHit()
    {
        // Запустити пошатування камери
        cameraShakeManager.Shake(shakeDuration, shakeMagnitude);
    }
}
