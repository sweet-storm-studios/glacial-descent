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

    //Character controller and rigidbody
    public new Rigidbody rigidbody;

    //PlayerManager class for managing player variables
    public PlayerManager playerManager;

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
        if(GetCurrentState() == jumpingState)
            jumpingState.DoTrick();
    }

    void OnStandUp()
    {
        if(GetCurrentState() == slippingState)
            slippingState.StandUp();
    }
}
