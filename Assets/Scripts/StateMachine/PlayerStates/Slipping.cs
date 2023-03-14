using UnityEngine;

public class Slipping : BaseState
{
    private MovementSM _sm;

    public Slipping(MovementSM stateMachine) : base("Slipping", stateMachine) { 
        _sm = (MovementSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        _sm.playerManager.setPlayerToGroundAngle();

        Vector3 vel = _sm.rigidbody.velocity;
        vel.x = _sm.speed * 0.5f;
        _sm.rigidbody.velocity = vel;
    }

    public void StandUp()
    {
        stateMachine.ChangeState(_sm.slideState);
    }
}