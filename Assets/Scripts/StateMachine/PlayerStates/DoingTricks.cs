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

        //Check if player is on the ground
        bool isGrounded = Physics.CheckSphere(_sm.groundCheck.position, 1f, _sm.groundLayer);
        Debug.DrawLine(new Vector3(0,0,0),new Vector3(1,1,1), Color.blue, 5f);

        if(isGrounded)
            stateMachine.ChangeState(_sm.slippingState);
        else if(Mathf.Abs(Input.GetAxis("Jump")) < Mathf.Epsilon)
            stateMachine.ChangeState(_sm.fallingState);

        _sm.transform.Rotate(Vector3.forward * 5);
    }

    public override void Exit()
    {
        base.Exit();
        _sm.transform.rotation = Quaternion.Euler(0,0,-5);
    }
}
