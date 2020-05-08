using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State initalState;
    [SerializeField] private State currentState;

    public void GoTo<T>() where T : State
    {
        currentState?.Deactivate();
        currentState = FindObjectOfType<T>();
        Debug.Log("Transition to " + currentState.GetType().ToString());
        currentState.Activate();
    }

    public void GoTo(State nextState)
    {
        currentState?.Deactivate();
        currentState = nextState;
        Debug.Log("Transition to " + currentState.GetType().ToString());
        currentState.Activate();
    }

    private void Start()
    {
        Debug.Log("Statemachine started");
        GoTo(initalState);
    }
}
