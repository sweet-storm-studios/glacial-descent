using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float force = 10;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        if (Input.GetKey("space"))
        {
            // _rigidbody.AddForce(Vector3.up * force);
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Physics hit something!");
    }
}