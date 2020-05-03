using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected StateMachine stateMachine;

    private void Awake()
    {
        stateMachine = FindObjectOfType<StateMachine>();
    }

    public abstract void Activate();
    public abstract void Deactivate();
}
