using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : State
{
    [SerializeField] private GameObject playground;

    PausedState()
    {
        menuFadeDuration = 0f;
    }

    public override void AfterActivate()
    {
        playground.SetActive(false);      
        Time.timeScale = 0f;
        // TODO adjust audio
    }

    public override void BeforeDeactivate()
    {
        Time.timeScale = 1f;
        playground.SetActive(true);
        // TODO adjust audio
    }

    public override void OnTrackerFound()
    {
        stateMachine.GoTo<GameState>();
    }
}
