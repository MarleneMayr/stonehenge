using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] protected Menu menu;
    protected float menuFadeDuration = 0.2f;

    protected StateMachine stateMachine;  
    private ImageTargetHandler imageTarget;

    protected virtual void Awake()
    {
        stateMachine = FindObjectOfType<StateMachine>();
    }

    public void Activate()
    {
        imageTarget = FindObjectOfType<ImageTargetHandler>();
        imageTarget.OnTrackerFound.AddListener(OnTrackerFound);
        imageTarget.OnTrackerLost.AddListener(OnTrackerLost);

        menu?.Show(menuFadeDuration);

        AfterActivate();
    }

    public void Deactivate()
    {
        BeforeDeactivate();

        imageTarget.OnTrackerFound.RemoveListener(OnTrackerFound);
        imageTarget.OnTrackerLost.RemoveListener(OnTrackerLost);

        menu?.Hide(menuFadeDuration);
    }

    public abstract void AfterActivate();
    public abstract void BeforeDeactivate();

    public virtual void OnTrackerFound() {}
    public virtual void OnTrackerLost() {}
}
