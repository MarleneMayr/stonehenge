using System.Collections;
using System.Collections.Generic;
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

    private void OnEnable()
    {
        Debug.Log("Statemachine enabled");
        GoTo(initalState);
    }
}
