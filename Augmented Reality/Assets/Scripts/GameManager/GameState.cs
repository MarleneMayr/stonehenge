using System;
using System.Collections;
using UnityEngine;

public class GameState : State
{
    [SerializeField] private MoldChecker moldChecker;
    [SerializeField] private Cookbook cookbook;

    private GameMenu gameMenu;
    private Timer timer;
    private int score = 0;

    protected override void Awake()
    {
        base.Awake();
        gameMenu = (GameMenu)menu;
        timer = FindObjectOfType<Timer>();
    }

    public override void AfterActivate()
    {
        Debug.Log("Gamestate activated");
        if (!timer.isRunning && !gameMenu.isCountdownOn)
        {
            gameMenu.StartCountdown(() => timer.StartTimer(100, gameMenu.SetTimerTxt, EndGame));

            NextRecipe();
        }
        
        moldChecker.OnMoldMatch.AddListener(NextRecipe);
        moldChecker.OnMoldMatch.AddListener(UpdateScore);
    }

    public override void BeforeDeactivate()
    {
        moldChecker.StopChecking();
        moldChecker.OnMoldMatch.RemoveListener(NextRecipe);
        moldChecker.OnMoldMatch.RemoveListener(UpdateScore);
    }

    private void NextRecipe()
    {
        Recipe next = cookbook.GetNext();
        //print("------------------------\nRECIPE: ");
        //foreach (var brick in next.Ingredients)
        //{
        //    print(brick.ToString());
        //}
        moldChecker.StartChecking(next);
        print("start next");
    }

    public override void OnTrackerLost()
    {
        stateMachine.GoTo<PausedState>();
    }

    private void UpdateScore()
    {
        score++;
        gameMenu.SetScoreTxt(score);
    }

    private void EndGame()
    {
        stateMachine.GoTo<HighscoreState>();
    }
}
