//Generic state class
public class BaseState
{
    public string name;
    protected StateMachine stateMachine;

    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }

    //Update runs on FixedUpdate (see StateMachine.cs)
    public virtual void Update() 
    { 

    }

    public virtual void Exit() { }
}
