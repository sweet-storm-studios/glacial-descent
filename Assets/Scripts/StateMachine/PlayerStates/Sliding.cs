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

        _sm.playerManager.setPlayerToGroundAngle();

        if (Mathf.Abs(Input.GetAxis("Jump")) > Mathf.Epsilon)
            stateMachine.ChangeState(_sm.jumpingState);
        else if(!_sm.playerManager.isGrounded)
            stateMachine.ChangeState(_sm.fallingState);

        _sm.playerManager.MovePlayer(_sm.speed);
    }
}
