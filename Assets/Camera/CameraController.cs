using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform CameraTarget;
    private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        CameraTarget = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - CameraTarget.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = CameraTarget.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
