using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : State
{
    [SerializeField] private GameObject playground;

    public override void AfterActivate()
    {
        playground.SetActive(false);
        StartCoroutine(PauseTimerAfterFade(menuFadeDuration));
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

    IEnumerator PauseTimerAfterFade(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0f;
    }
}
