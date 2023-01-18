using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5.0f;
    public float rotationSpeed = 1.0f;
    public float distance = 10.0f;
    public float height = 5.0f;


    void LateUpdate()
    {

        Vector3 targetVelocity = target.GetComponent<Rigidbody>().velocity;
        Vector3 targetPosition = target.position + targetVelocity * Time.deltaTime;

        Vector3 offset = new Vector3(0, height, -distance);
        offset = Quaternion.LookRotation(targetVelocity) * offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition + offset, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetVelocity), rotationSpeed * Time.deltaTime);
    }
}
