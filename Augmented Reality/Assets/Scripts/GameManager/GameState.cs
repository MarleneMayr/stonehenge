using System;
using System.Collections;
using UnityEngine;

public class GameState : State
{
    [SerializeField] private MoldChecker moldChecker;
    [SerializeField] private Cookbook cookbook;

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
        if (!timer.isRunning && !gameMenu.isCountdownOn)
        {
            gameMenu.StartCountdown(() => timer.StartTimer(100, gameMenu.SetTimerTxt, EndGame));

            NextRecipe();
        }
     
        moldChecker.OnMoldMatch.AddListener(NextRecipe);
        moldChecker.OnMoldMatch.AddListener(UpdateScore);

        selectionManager.Activate();
        gameMenu.ScreenTapped.AddListener(selectionManager.HandleTap);
    }

    public override void BeforeDeactivate()
    {
        moldChecker.StopChecking();
        moldChecker.OnMoldMatch.RemoveListener(NextRecipe);
        moldChecker.OnMoldMatch.RemoveListener(UpdateScore);

        gameMenu.ScreenTapped.RemoveListener(selectionManager.HandleTap);
        selectionManager.Deactivate();
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
