using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreState : State
{
    [SerializeField] private GameObject playground;
    [SerializeField] private Highscore highscore;
    private HighscoreMenu highscoreMenu;
    private AudioManager audioManager;

    private dreamloLeaderBoard.Score previousBest;
    private dreamloLeaderBoard.Score personalHighscore;
    private List<dreamloLeaderBoard.Score> topFive;

    protected override void Awake()
    {
        base.Awake();
        highscoreMenu = (HighscoreMenu)menu;
        audioManager = FindObjectOfType<AudioManager>();
    }

    public override void AfterActivate()
    {
        highscoreMenu.ShowScore(highscore.gameScore);
        highscoreMenu.ShowNamePanel(highscore.playerName);
        highscoreMenu.OnPlayAgainPressed.AddListener(PlayAgain);
        highscoreMenu.OnSubmitName.AddListener(StoreScoreAndShowLeaderboard);
    }

    public override void BeforeDeactivate()
    {
        highscoreMenu.OnPlayAgainPressed.RemoveListener(PlayAgain);
        highscoreMenu.OnSubmitName.RemoveListener(StoreScoreAndShowLeaderboard);
        highscoreMenu.Hide(0);
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
        audioManager.Play(AudioManager.GlobalSound.Click);
        highscore.playerName = name;
        Debug.Log("Name set to " + highscore.playerName);
        highscore.GetPersonalBest(GotPreviousBest);       
    }

    private void GotPreviousBest(dreamloLeaderBoard.Score previousBest)
    {
        this.previousBest = previousBest;
        highscore.UploadScore(AfterUpload); // once this is finished call action to receive rest and display
    }

    private void AfterUpload()
    {
        personalHighscore = highscore.GetPersonalRanking();
        topFive = highscore.GetTopFive();

        highscoreMenu.ShowOnlineHighscores(topFive, personalHighscore, previousBest, highscore.gameScore);
    }
}
