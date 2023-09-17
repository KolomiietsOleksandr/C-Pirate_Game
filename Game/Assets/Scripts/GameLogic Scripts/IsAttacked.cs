using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAttacked : MonoBehaviour
{
    public int AttackCheker = 0;

    public CameraShakeManager cameraShakeManager;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;

    public void Start()
    {
        cameraShakeManager = GameObject.Find("Main Camera").GetComponent<CameraShakeManager>();
    }

    public void OnHitShake()
    {
        // Запустити пошатування камери
        cameraShakeManager.Shake(shakeDuration, shakeMagnitude);
    }

    public void Attacked()
    {
        AttackCheker = 1;
    }

    public void AttackReset()
    {
        AttackCheker = 0;
    }
}
