using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameState : State
{
    [SerializeField] private MoldChecker moldChecker;
    [SerializeField] private Cookbook cookbook;
    [SerializeField] private RecipeVisualizer visualizer;
    [SerializeField] private GameObject playground;

    [SerializeField] private CountdownMenu countdownMenu;
    [SerializeField] private GameMenu gameMenu;
    [SerializeField] private SingleMessageMenu pausedMenu;

    [SerializeField] private bool keepSelectionOnPause;

    private Timer timer;
    private SelectionManager selectionManager;
    private AudioManager audioManager;
    private int score = 0;

    protected override void Awake()
    {
        base.Awake();
        timer = FindObjectOfType<Timer>();
        selectionManager = FindObjectOfType<SelectionManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public override void AfterActivate()
    {
        score = 0;

        countdownMenu.StartCountdown(StartGame);
        gameMenu.SetTimerWarning(false);
        audioManager.PlayOnce(AudioManager.GlobalSound.Countdown);
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

        // TODO Audio here or in EndGame() ?
        audioManager.Play(AudioManager.GlobalSound.GameOver);
        audioManager.Stop(AudioManager.GlobalSound.TickingLoop);
        audioManager.Stop(AudioManager.GlobalSound.Last10Seconds);
    }

    private void StartGame()
    {
        gameMenu.Show();

        timer.StartTimer(15, EndGame);
        timer.OnTimerTick.AddListener(UpdateTime);
        audioManager.PlayOnce(AudioManager.GlobalSound.TickingLoop);

        selectionManager.Activate();
        gameMenu.ScreenTapped.AddListener(selectionManager.HandleTap);

        Recipe next = cookbook.GetNext();
        visualizer.ShowRecipe(next);
        moldChecker.StartChecking(next);
        moldChecker.OnMoldMatch.AddListener(NextRecipe);
        moldChecker.OnMoldMatch.AddListener(UpdateScore);
    }

    private void NextRecipe(int ingredientCount)
    {
        var newTime = timer.AddToTimer(10); // 10 bonus seconds per fulfilled recipe
        UpdateTime(newTime);

        Recipe next = cookbook.GetNext();
        visualizer.ShowRecipe(next);
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

        audioManager.PauseIfPlaying(AudioManager.GlobalSound.Countdown);
        audioManager.PauseIfPlaying(AudioManager.GlobalSound.TickingLoop);
    }

    public void Unpause()
    {
        playground.SetActive(true);
        Time.timeScale = 1f;

        pausedMenu.Hide(0);

        if (!countdownMenu.isRunning) gameMenu.Show();
        if (!keepSelectionOnPause) selectionManager.Activate();

        audioManager.ResumeIfPaused(AudioManager.GlobalSound.Countdown);
        audioManager.ResumeIfPaused(AudioManager.GlobalSound.TickingLoop);
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
