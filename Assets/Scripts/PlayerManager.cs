using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool isGrounded;
    public bool collidedWithObstacle;

    //Used for ground & obstacle collision check
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform obstacleCheck;
    public LayerMask obstacleLayer;

    void Start(){ }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 1f, groundLayer);
        collidedWithObstacle = Physics.CheckSphere(obstacleCheck.position, 1f, obstacleLayer);
    }
}
