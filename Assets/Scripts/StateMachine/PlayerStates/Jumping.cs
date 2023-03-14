using UnityEngine;

public class Jumping : BaseState
{
    private MovementSM _sm;

    public Jumping(MovementSM stateMachine) : base("Jumping", stateMachine) { 
        _sm = (MovementSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _sm.rigidbody.AddForce(Vector3.up * _sm.jumpForce, ForceMode.Impulse);
    }

    public override void Update()
    {
        base.Update();

        //Check if player is on the ground
        bool isGrounded = Physics.CheckSphere(_sm.groundCheck.position, 1f, _sm.groundLayer);

        if(isGrounded)
            stateMachine.ChangeState(_sm.slideState);

        Vector3 vel = _sm.rigidbody.velocity;
        vel.x = _sm.speed;
        _sm.rigidbody.velocity = vel;
    }

    public void DoTrick()
    {
        stateMachine.ChangeState(_sm.trickState);
    }
}