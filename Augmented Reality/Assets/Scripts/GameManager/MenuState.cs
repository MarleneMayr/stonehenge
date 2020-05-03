using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State
{
    [SerializeField] private MainMenu mainMenu;

    public override void Activate()
    {
        mainMenu.Show();
        mainMenu.OnStartClicked.AddListener(StartGame);
    }

    public override void Deactivate()
    {
        mainMenu.Hide();
        mainMenu.OnStartClicked.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        stateMachine.GoTo<GameState>();
    }
}
