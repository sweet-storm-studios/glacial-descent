using UnityEngine;

//Generic State Machine class
public class StateMachine : MonoBehaviour
{
    //Base state of the machine
    BaseState currentState;

    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    //Frame rate independent update
    void FixedUpdate()
    {
        if(currentState != null)
            currentState.Update();
    }

    //Swap the current state of the machine
    public void ChangeState(BaseState newState)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }

    //Write text with current status on screen
    private void OnGUI()
    {
        string content = currentState != null ? currentState.name : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
}
