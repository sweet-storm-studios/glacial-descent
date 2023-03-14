using UnityEngine;
using UnityEngine.InputSystem;

public class Sliding : BaseState
{
    private MovementSM _sm;

    public Sliding(MovementSM stateMachine) : base("Sliding", stateMachine) { 
        _sm = (MovementSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        //Check if player is on the ground
        bool isGrounded = Physics.CheckSphere(_sm.groundCheck.position, 1f, _sm.groundLayer);

        if (Mathf.Abs(Input.GetAxis("Jump")) > Mathf.Epsilon)
            stateMachine.ChangeState(_sm.jumpingState);
        else if(!isGrounded)
            stateMachine.ChangeState(_sm.fallingState);
        
        
        Vector3 vel = _sm.rigidbody.velocity;
        vel.x = _sm.speed;
        _sm.rigidbody.velocity = vel;
    }
}
