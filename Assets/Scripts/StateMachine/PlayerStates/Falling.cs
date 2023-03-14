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

        if(_sm.playerManager.isGrounded)
            stateMachine.ChangeState(_sm.slideState);

        Vector3 vel = _sm.rigidbody.velocity;
        vel.x = _sm.speed;
        _sm.rigidbody.velocity = vel;
    }
}
