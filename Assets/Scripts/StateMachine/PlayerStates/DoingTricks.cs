using UnityEngine;

public class DoingTricks : BaseState
{
    private MovementSM _sm;

    public DoingTricks(MovementSM stateMachine) : base("Doing MAD Tricks", stateMachine) { 
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
            stateMachine.ChangeState(_sm.slippingState);
        else if(Mathf.Abs(Input.GetAxis("Jump")) < Mathf.Epsilon)
            stateMachine.ChangeState(_sm.fallingState);

        _sm.transform.Rotate(Vector3.forward * 5);
    }
}
