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
        _sm.playerManager.MovePlayer(_sm.speed * _sm.slippingSpeedDecrease);
    }

    public void StandUp()
    {
        stateMachine.ChangeState(_sm.slideState);
    }
}