using UnityEngine;
using System;
using System.Collections.Generic;

public class Highscore : MonoBehaviour
{
    public int gameScore = 0;
    public string playerName = "";

    dreamloLeaderBoard dreamlo;

    private Action<dreamloLeaderBoard.Score> returnPersonalBestAction;

    void Start()
    {
        dreamlo = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
    }

    public void UploadScore(Action onDataAvailable)
    {
        if (dreamlo.publicCode == "") Debug.LogError("Dreamlo: publicCode missing");
        if (dreamlo.privateCode == "") Debug.LogError("Dreamlo: privateCode missing");

        dreamlo.AddScore(playerName, gameScore, onDataAvailable);
    }

    public List<dreamloLeaderBoard.Score> GetTopFive()
    {
        List<dreamloLeaderBoard.Score> highscore = dreamlo.ToListHighToLow();
        return highscore?.GetRange(0, Mathf.Min(5, highscore.Count));
    }

    public dreamloLeaderBoard.Score GetPersonalRanking()
    {
        List<dreamloLeaderBoard.Score> highscore = dreamlo.ToListHighToLow();
        dreamloLeaderBoard.Score currentRanking = highscore.Find(score => score.playerName == playerName);
        if (currentRanking.playerName == null) Debug.LogWarning("Dreamlo: Personal ranking could not be retrieved! ");
        return currentRanking;
    }

    public void GetPersonalBest(Action<dreamloLeaderBoard.Score> returnAction)
    {
        returnPersonalBestAction = returnAction;
        dreamlo.GetScores(ReturnPersonalBest);
    }

    private void ReturnPersonalBest()
    {
        List<dreamloLeaderBoard.Score> highscore = dreamlo.ToListHighToLow();

        if (highscore.Count > 0)
        {
            dreamloLeaderBoard.Score previousBest = highscore.Find(score => score.playerName == playerName);
            returnPersonalBestAction(previousBest);
        }
        else
        {
            Debug.LogWarning("Dreamlo: Previous Highscore List could not be loaded (this is ok if it is your first time playing with this name)");
        }
    }

}

