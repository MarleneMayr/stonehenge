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
    private SelectionManager selectionManager;
    private int score = 0;

    protected override void Awake()
    {
        base.Awake();
        gameMenu = (GameMenu)menu;
        timer = FindObjectOfType<Timer>();
        selectionManager = FindObjectOfType<SelectionManager>();
    }

    public override void AfterActivate()
    {
        Debug.Log("Gamestate activated");
        if (!timer.isRunning && !countdownMenu.isRunning)
        {
            InitializeGame();
        }
        //else if ( countdownMenu.isRunning)
        //{
        //    countdownMenu.Show();
        //}

        gameMenu.SetTimerWarning(false);
        selectionManager.Activate();
        gameMenu.ScreenTapped.AddListener(selectionManager.HandleTap);
    }

    public override void BeforeDeactivate()
    {
        countdownMenu.Hide(0);
        moldChecker.StopChecking();

        timer.OnTimerTick.RemoveListener(UpdateTime);
        moldChecker.OnMoldMatch.RemoveListener(NextRecipe);
        moldChecker.OnMoldMatch.RemoveListener(UpdateScore);

        gameMenu.ScreenTapped.RemoveListener(selectionManager.HandleTap);
        selectionManager.Deactivate();
    }

    private void InitializeGame()
    {
        score = 0;
        gameMenu.Hide(0); // hide immediately to show only countdown
        countdownMenu.StartCountdown(StartGame);
        moldChecker.StartChecking(cookbook.GetNext());

        moldChecker.OnMoldMatch.AddListener(NextRecipe);
        moldChecker.OnMoldMatch.AddListener(UpdateScore);
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
