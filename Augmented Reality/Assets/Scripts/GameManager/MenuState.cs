using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State
{
    private MainMenu mainMenu;

    protected override void Awake()
    {
        base.Awake();
        mainMenu = (MainMenu)menu;
    }

    public override void AfterActivate()
    {
        Debug.Log("Menustate activated");
        mainMenu.OnStartClicked.AddListener(StartGame);
    }

    public override void BeforeDeactivate()
    {
        mainMenu.OnStartClicked.RemoveListener(StartGame);
    }

    public override void OnTrackerLost()
    {
        stateMachine.GoTo<StartupState>();
    }

    private void StartGame()
    {
        stateMachine.GoTo<GameState>();
    }
}
