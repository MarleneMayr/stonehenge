using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : State
{
    [SerializeField] private GameObject playground;
    AudioManager audioManager;

    protected override void Awake()
    {
        base.Awake();
        audioManager = FindObjectOfType<AudioManager>();
        menuFadeDuration = 0f;
    }

    public override void AfterActivate()
    {
        playground.SetActive(false);      
        Time.timeScale = 0f;
        audioManager.Pause(AudioManager.GlobalSound.TickingLoop);
        audioManager.Pause(AudioManager.GlobalSound.Last10Seconds);
    }

    public override void BeforeDeactivate()
    {
        Time.timeScale = 1f;
        playground.SetActive(true);
        audioManager.ResumeIfPaused(AudioManager.GlobalSound.TickingLoop);
        audioManager.ResumeIfPaused(AudioManager.GlobalSound.Last10Seconds);
    }

    public override void OnTrackerFound()
    {
        stateMachine.GoTo<GameState>();
    }
}
