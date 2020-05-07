﻿using System.Collections;
using UnityEngine;

public class StartupState : State
{
    [SerializeField] private GameObject playground;

    public override void AfterActivate()
    {
        playground.SetActive(false);
        StartCoroutine(PauseTimerAfterFade(menuFadeDuration));
    }

    public override void BeforeDeactivate()
    {
        Time.timeScale = 1f;
        playground.SetActive(true);
    }

    public override void OnTrackerFound()
    {
        stateMachine.GoTo<MenuState>();
    }

    IEnumerator PauseTimerAfterFade(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0f;
    }
}
