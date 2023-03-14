using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 8;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float gravity = -20;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool ableToDoubleJump = true;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * speed;

        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
        direction.y += gravity * Time.deltaTime;
        if(isGrounded)
        {
            // direction.y = 0.5f;
            ableToDoubleJump = true;
            if(Input.GetButtonDown("Jump"))
            {
                Jump(0);
            }
        } 
        else 
        {
            direction.y += gravity * Time.deltaTime;
            if(ableToDoubleJump && Input.GetButtonDown("Jump"))
            {
                Jump(6);
                ableToDoubleJump = false;
            }
        }
        
        
        controller.Move(direction * Time.deltaTime);
    }

    void Jump(float bonusForce)
    {
      direction.y = jumpForce + bonusForce;  
    }
}
