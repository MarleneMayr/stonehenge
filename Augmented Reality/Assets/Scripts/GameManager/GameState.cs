using UnityEngine;

public class GameState : State
{
    [SerializeField] private MoldChecker moldChecker;
    [SerializeField] private Cookbook cookbook;
    [SerializeField] private RecipeVisualizer visualizer;
    [SerializeField] private GameObject playground;
    [SerializeField] private Floor floor;
    [SerializeField] private Highscore highscore;

    [SerializeField] private CountdownMenu countdownMenu;
    [SerializeField] private GameMenu gameMenu;
    [SerializeField] private SingleMessageMenu pausedMenu;

    [SerializeField] private bool keepSelectionOnPause;

    private Timer timer;
    private SelectionManager selectionManager;
    private AudioManager audioManager;

    protected override void Awake()
    {
        base.Awake();
        timer = FindObjectOfType<Timer>();
        selectionManager = FindObjectOfType<SelectionManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public override void AfterActivate()
    {
        playground.SetActive(true);
        highscore.gameScore = 0;
        gameMenu.SetScoreTxt(0);

        countdownMenu.StartCountdown(StartGame);
        gameMenu.SetTimerWarning(false);
        audioManager.PlayOnce(AudioManager.GlobalSound.Countdown);

        Recipe next = cookbook.GetNext();
        visualizer.ShowRecipe(next);

        moldChecker.StartChecking(next);
        moldChecker.OnMoldMatch.AddListener(NextRecipe);
        moldChecker.OnMoldMatch.AddListener(UpdateScore);

        if (imageTarget.trackerLost)
        {
            Pause();
        }
    }

    public override void BeforeDeactivate()
    {
        countdownMenu.Hide();
        gameMenu.Hide();
        pausedMenu.Hide();

        moldChecker.StopChecking();
        floor.ToggleTimeWarning(false);

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

        timer.StartTimer(60, EndGame);
        timer.OnTimerTick.AddListener(UpdateTime);
        audioManager.PlayOnce(AudioManager.GlobalSound.TickingLoop);

        selectionManager.Activate();
        gameMenu.ScreenTapped.AddListener(selectionManager.HandleTap);
    }

    private void NextRecipe(int ingredientCount)
    {
        var newTime = timer.AddToTimer(10); // 10 bonus seconds per fulfilled recipe
        UpdateTime(newTime);

        floor.HighlightSuccess();

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

        playground.transform.localScale = new Vector3(0, 0, 0);
        //playground.SetActive(false);
        Time.timeScale = 0f;

        audioManager.PauseIfPlaying(AudioManager.GlobalSound.Countdown);
        audioManager.PauseIfPlaying(AudioManager.GlobalSound.Last10Seconds);
        audioManager.PauseIfPlaying(AudioManager.GlobalSound.TickingLoop);
    }

    public void Unpause()
    {
        //playground.SetActive(true);
        playground.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        Time.timeScale = 1f;
        pausedMenu.Hide(0);

        if (!countdownMenu.isRunning) gameMenu.Show();
        if (!keepSelectionOnPause) selectionManager.Activate();

        audioManager.ResumeIfPaused(AudioManager.GlobalSound.Countdown);
        audioManager.ResumeIfPaused(AudioManager.GlobalSound.Last10Seconds);
        audioManager.ResumeIfPaused(AudioManager.GlobalSound.TickingLoop);
    }

    private void UpdateTime(int time)
    {
        gameMenu.SetTimerTxt(time);
        gameMenu.SetTimerWarning(time <= 10);
        floor.ToggleTimeWarning(time <= 10);
        if (time == 10) audioManager.PlayOnce(AudioManager.GlobalSound.Last10Seconds);
        if (time > 10) audioManager.Stop(AudioManager.GlobalSound.Last10Seconds);
    }

    private void UpdateScore(int ingredientCount)
    {
        highscore.gameScore += ingredientCount * 100;
        gameMenu.SetScoreTxt(highscore.gameScore);
    }

    private void EndGame()
    {
        stateMachine.GoTo<HighscoreState>();
    }
}
