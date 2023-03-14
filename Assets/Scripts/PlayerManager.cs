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

    //For determining ground angle
    public Transform rearRayPosition;
    public Transform frontRayPosition;

    void Start(){ }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 1f, groundLayer);
        collidedWithObstacle = Physics.CheckSphere(obstacleCheck.position, 1f, obstacleLayer);
    }

    public void setPlayerToGroundAngle()
    {
        //Get angle of ground and object
        float angle = getGroundAngle();

        //Set player angle to ground angle
        Vector3 newRotation = new Vector3(0, 0, angle);
        transform.eulerAngles = newRotation;
    }

    private float getGroundAngle()
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
        }

        RaycastHit frontHit;
        Vector3 frontRayStartPos = new Vector3(frontRayPosition.position.x, rearRayPosition.position.y, frontRayPosition.position.z);
        if(Physics.Raycast(frontRayStartPos, frontRayPosition.TransformDirection(-Vector3.up), out frontHit, Mathf.Infinity, groundLayer))
        {
            Debug.DrawRay(frontRayStartPos, frontRayPosition.TransformDirection(-Vector3.up) * frontHit.distance, Color.yellow);
        }

        if(frontHit.distance > rearHit.distance)
        {
            groundAngle = -1 * groundAngle;
        }
        
        return groundAngle;
    }
}
