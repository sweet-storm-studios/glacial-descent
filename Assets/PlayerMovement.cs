using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float speed = 2;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0, z);
        transform.Translate(movement * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player hit something! This is a test change");
    }
}
