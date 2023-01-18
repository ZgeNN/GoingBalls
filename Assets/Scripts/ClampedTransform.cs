using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampedTransform : MonoBehaviour
{
    public float moveSpeed=3;
    public float moveDistance=10;
    private float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        float newX = startX + Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
