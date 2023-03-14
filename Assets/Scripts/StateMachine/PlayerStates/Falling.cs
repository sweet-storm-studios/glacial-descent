using UnityEngine;

public class Falling : BaseState
{
    private MovementSM _sm;

    public Falling(MovementSM stateMachine) : base("Falling", stateMachine) { 
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

        if(isGrounded)
            stateMachine.ChangeState(_sm.slideState);

        Vector3 vel = _sm.rigidbody.velocity;
        vel.x = _sm.speed;
        _sm.rigidbody.velocity = vel;
    }
}
