using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameState : State
{
    [SerializeField] private MoldChecker moldChecker;
    [SerializeField] private Cookbook cookbook;
    [SerializeField] private GameObject playground;

    [SerializeField] private CountdownMenu countdownMenu;
    [SerializeField] private GameMenu gameMenu;
    [SerializeField] private SingleMessageMenu pausedMenu;

    [SerializeField] private bool keepSelectionOnPause;

    private Timer timer;
    private SelectionManager selectionManager;
    private int score = 0;

    protected override void Awake()
    {
        base.Awake();
        timer = FindObjectOfType<Timer>();
        selectionManager = FindObjectOfType<SelectionManager>();
    }

    public override void AfterActivate()
    {
        score = 0;

        countdownMenu.StartCountdown(StartGame);
        gameMenu.SetTimerWarning(false);
    }

    public override void BeforeDeactivate()
    {
        countdownMenu.Hide();
        gameMenu.Hide();
        pausedMenu.Hide();

        moldChecker.StopChecking();

        timer.OnTimerTick.RemoveListener(UpdateTime);
        moldChecker.OnMoldMatch.RemoveListener(NextRecipe);
        moldChecker.OnMoldMatch.RemoveListener(UpdateScore);

        gameMenu.ScreenTapped.RemoveListener(selectionManager.HandleTap);
        selectionManager.Deactivate();
    }

    private void StartGame()
    {
        gameMenu.Show();

        timer.StartTimer(15, EndGame);
        timer.OnTimerTick.AddListener(UpdateTime);

        selectionManager.Activate();
        gameMenu.ScreenTapped.AddListener(selectionManager.HandleTap);

        moldChecker.StartChecking(cookbook.GetNext());
        moldChecker.OnMoldMatch.AddListener(NextRecipe);
        moldChecker.OnMoldMatch.AddListener(UpdateScore);
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
        Pause();
    }

    public override void OnTrackerFound()
    {
        Unpause();
    }

    public void Pause()
    {
        gameMenu.Hide(0);
        countdownMenu.Hide(0);
        pausedMenu.Show(0);

        if (!keepSelectionOnPause) selectionManager.Deactivate();

        playground.SetActive(false);
        Time.timeScale = 0f;

        // TODO pause timer audio
    }

    public void Unpause()
    {
        playground.SetActive(true);
        Time.timeScale = 1f;

        pausedMenu.Hide(0);

        if (!countdownMenu.isRunning) gameMenu.Show();
        if (!keepSelectionOnPause) selectionManager.Activate();

        // TODO pause timer audio
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
        // TODO play sound
        // TODO drop brick if still one is selected
        // TODO maybe deactivate playgroun? => check how it looks with playground on
        // TODO actually the above 3 things could be done in deactivate now
        stateMachine.GoTo<HighscoreState>();
    }
}
