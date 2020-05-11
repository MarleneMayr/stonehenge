using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State
{
    private MainMenu mainMenu;
    private AudioManager audioManager;

    protected override void Awake()
    {
        base.Awake();
        mainMenu = (MainMenu)menu;
        audioManager = FindObjectOfType<AudioManager>();
    }

    public override void AfterActivate()
    {
        mainMenu.OnStartClicked.AddListener(StartGame);
    }

    public override void BeforeDeactivate()
    {
        mainMenu.OnStartClicked.RemoveListener(StartGame);
        audioManager.Stop(AudioManager.GlobalSound.BirdsLoop);
    }

    public override void OnTrackerLost()
    {
        stateMachine.GoTo<StartupState>();
    }

    private void StartGame()
    {
        audioManager.Play(AudioManager.GlobalSound.Click);
        stateMachine.GoTo<GameState>();
    }
}
