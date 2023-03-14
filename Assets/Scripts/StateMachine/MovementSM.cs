using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSM : StateMachine
{
    //All available states
    [HideInInspector]
    public Sliding slideState;
    [HideInInspector]
    public Jumping jumpingState;
    [HideInInspector]
    public DoingTricks trickState;
    [HideInInspector]
    public Falling fallingState;
    [HideInInspector]
    public Slipping slippingState;

    //Variables to control moving and jumping; editable in inspector
    public float speed = 4f;
    public float jumpForce = 5f;

    //Used for ground & obstacle collision check
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform obstacleCheck;
    public LayerMask obstacleLayer;

    //Character controller and rigidbody
    public new Rigidbody rigidbody;

    private void Awake()
    {
        slideState = new Sliding(this);
        jumpingState = new Jumping(this); 
        trickState = new DoingTricks(this);
        fallingState = new Falling(this);
        slippingState = new Slipping(this);
    }

    protected override BaseState GetInitialState()
    {
        //Set this machine's base state to Sliding
        return slideState;
    }

    void OnTrick()
    {
        jumpingState.DoTrick();
    }

    void OnStandUp()
    {
        slippingState.StandUp();
    }
}
