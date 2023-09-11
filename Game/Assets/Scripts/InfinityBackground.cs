using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityBackground : MonoBehaviour
{
    public float speed = 1f;
    Vector3 StartPosition;
    float RepeatWidth;
    public int size;

    void Start()
    {
        StartPosition = transform.position;
        RepeatWidth = GetComponent<BoxCollider2D>().size.x / size;
    }

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        if (transform.position.x < StartPosition.x - RepeatWidth)
        {
            transform.position = StartPosition;
        }
    }
}
