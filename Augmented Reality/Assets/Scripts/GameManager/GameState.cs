using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameState : State
{
    [SerializeField] private MoldChecker moldChecker;
    [SerializeField] private Cookbook cookbook;
    [SerializeField] private CountdownMenu countdownMenu;

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
        if (!timer.isRunning && !countdownMenu.isRunning)
        {
            InitializeGame();
        }

        gameMenu.SetTimerWarning(false);
        moldChecker.OnMoldMatch.AddListener(NextRecipe);
        moldChecker.OnMoldMatch.AddListener(UpdateScore);
    }

    public override void BeforeDeactivate()
    {
        gameMenu.Hide();
        moldChecker.StopChecking();

        timer.OnTimerTick.RemoveListener(UpdateTime);
        moldChecker.OnMoldMatch.RemoveListener(NextRecipe);
        moldChecker.OnMoldMatch.RemoveListener(UpdateScore);
    }

    private void InitializeGame()
    {
        score = 0;
        gameMenu.Hide(0); // hide immediately to show only countdown
        countdownMenu.StartCountdown(StartGame);
        moldChecker.StartChecking(cookbook.GetNext());
    }

    private void StartGame()
    {
        gameMenu.Show();
        timer.StartTimer(15, EndGame);
        timer.OnTimerTick.AddListener(UpdateTime);
    }

    private void NextRecipe(int ingredientCount)
    {
        var newTime = timer.AddToTimer(10); // 10 bonus seconds per fulfilled recipe
        UpdateTime(newTime);

        Recipe next = cookbook.GetNext();
        moldChecker.StartChecking(next);
        print("start next recipe");
    }

    public override void OnTrackerLost()
    {
        stateMachine.GoTo<PausedState>();
    }

    private void UpdateTime(int time)
    {
        gameMenu.SetTimerTxt(time);
        gameMenu.SetTimerWarning(time <= 10);
    }

    private void UpdateScore(int ingredientCount)
    {
        score += ingredientCount * 100;
        gameMenu.SetScoreTxt(score);
    }

    private void EndGame()
    {
        stateMachine.GoTo<HighscoreState>();
    }
}
