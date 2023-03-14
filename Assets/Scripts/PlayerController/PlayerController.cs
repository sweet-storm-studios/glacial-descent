using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Responsible for controlling the character
    [SerializeField] private CharacterController controller;
    
    //For determining ground contact & angle
    [SerializeField] private Transform rearRayPosition;
    [SerializeField] private Transform frontRayPosition;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform obstacleCheck;
    [SerializeField] private LayerMask obstacleLayer;


    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private float maxJumpTime;
    
    [SerializeField] private float currentSpeed;
    [SerializeField] private float currentJumpForce;
    private float jumpTime;
    private bool isJumping;
    private bool uphill;
    private bool flatSurface;
    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        //Gravity is calculated using a negative number in script
        gravity = -1 * gravity;

        //Set base speed
        currentSpeed = minSpeed;
        currentJumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is on ground
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        //Check if player collides with obstacle
        bool collidedWithObstacle = Physics.CheckSphere(obstacleCheck.position, 0.2f, obstacleLayer);

        if(collidedWithObstacle)
        {
            Debug.Log("collided");
            controller.Move(new Vector3(-20,0,0));
            //GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        }

        if(isGrounded)
        {
            //Get angle of ground and object
            float angle = getGroundAngle();

            //Set player angle to ground angle
            Vector3 newRotation = new Vector3(0, 0, angle);
            transform.eulerAngles = newRotation;
            
            //Apply speed based on angle
            if(!uphill && !flatSurface)
            {
                //Going downhill
                currentSpeed += (0.5f * (-1 * angle)) * Time.deltaTime;
            }
            else if(uphill && !flatSurface)
            {
                //Going uphill
                currentSpeed -= (0.5f * angle) * Time.deltaTime;
            }
        }

        //If uphill, increase jump force
        if(uphill)
        {
            currentJumpForce *= 2.5f;
        }
        
        //Jumping logic
        checkJump(isGrounded);

        // Check min and max speed
        if(currentSpeed < minSpeed)
        {
            currentSpeed = minSpeed;
        }
        else if(currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }

        if(currentJumpForce > jumpForce * 2.5f)
        {
            currentJumpForce = jumpForce * 2.5f;
        }

        //Applies gravity to player
        direction.y += gravity * Time.deltaTime;

        //Horizontal movement
        direction.x = currentSpeed;

        //Apply all movements at the end of frame
        controller.Move(direction * Time.deltaTime);
    }

    float getGroundAngle()
    { 
        float groundAngle = 0;

        rearRayPosition.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);
        frontRayPosition.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);

        RaycastHit rearHit;
        if(Physics.Raycast(rearRayPosition.position, rearRayPosition.TransformDirection(-Vector3.up), out rearHit, Mathf.Infinity, groundLayer))
        {
            Debug.DrawRay(rearRayPosition.position, rearRayPosition.TransformDirection(-Vector3.up) * rearHit.distance, Color.yellow);
            groundAngle = Vector3.Angle(rearHit.normal, Vector3.up);
        }
        else
        {
            Debug.DrawRay(rearRayPosition.position, rearRayPosition.TransformDirection(-Vector3.up) * 1000, Color.red);
            uphill = false;
            flatSurface = false;
        }

        RaycastHit frontHit;
        Vector3 frontRayStartPos = new Vector3(frontRayPosition.position.x, rearRayPosition.position.y, frontRayPosition.position.z);
        if(Physics.Raycast(frontRayStartPos, frontRayPosition.TransformDirection(-Vector3.up), out frontHit, Mathf.Infinity, groundLayer))
        {
            Debug.DrawRay(frontRayStartPos, frontRayPosition.TransformDirection(-Vector3.up) * frontHit.distance, Color.yellow);
        }
        else
        {
            uphill = true;
            flatSurface = false;
        }

        if(frontHit.distance < rearHit.distance)
        {
            uphill = true;
            flatSurface = false;
        }
        else if(frontHit.distance > rearHit.distance)
        {
            uphill = false;
            flatSurface = false;
            groundAngle = -1 * groundAngle;
        }
        else if(frontHit.distance == rearHit.distance)
        {
            flatSurface = true;
        }
        
        return groundAngle;
    }

    void checkJump(bool isGrounded)
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            jumpTime = maxJumpTime;
            direction.y = currentJumpForce;
        }

        if(Input.GetButton("Jump") && isJumping)
        {
            if(jumpTime > 0)
            {
                direction.y = currentJumpForce;
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }
}
