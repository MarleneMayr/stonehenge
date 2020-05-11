using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreState : State
{
    [SerializeField] private GameObject playground;
    [SerializeField] private Highscore highscore;
    private HighscoreMenu highscoreMenu;
    private AudioManager audioManager;

    protected override void Awake()
    {
        base.Awake();
        highscoreMenu = (HighscoreMenu)menu;
        audioManager = FindObjectOfType<AudioManager>();
    }

    public override void AfterActivate()
    {
        highscoreMenu.ShowScore(highscore.gameScore);
        highscoreMenu.ShowNamePanel();
        highscoreMenu.OnPlayAgainPressed.AddListener(PlayAgain);
        highscoreMenu.OnSubmitName.AddListener(StoreScoreAndShowLeaderboard);
    }

    public override void BeforeDeactivate()
    {

    }

    public override void OnTrackerLost()
    {
        playground.SetActive(false);
    }

    public override void OnTrackerFound()
    {
        playground.SetActive(true);
    }

    private void PlayAgain()
    {
        audioManager.Play(AudioManager.GlobalSound.Click);
        stateMachine.GoTo<GameState>();
    }

    private void StoreScoreAndShowLeaderboard(string name)
    {
        highscore.playerName = name;

        dreamloLeaderBoard.Score previousBest = highscore.GetPersonalBest();
        highscore.UploadScore();
        List<dreamloLeaderBoard.Score> scores = highscore.GetTopFive();
        dreamloLeaderBoard.Score currentScore = highscore.GetCurrentRanking();

        highscoreMenu.ShowOnlineHighscores(scores, currentScore, previousBest);
    }
}
