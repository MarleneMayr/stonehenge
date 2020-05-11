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

    public void UploadScore()
    {
        if (dreamlo.publicCode == "") Debug.LogError("Dreamlo: publicCode missing");
        if (dreamlo.privateCode == "") Debug.LogError("Dreamlo: privateCode missing");

        dreamlo.AddScore(playerName, gameScore);
    }

    public List<dreamloLeaderBoard.Score> GetTopFive()
    {
        List<dreamloLeaderBoard.Score> highscore = dreamlo.ToListHighToLow();
        Debug.Log("Highscore length: " + highscore.Count);
        return highscore?.GetRange(0, Mathf.Min(5, highscore.Count));
    }

    public dreamloLeaderBoard.Score GetCurrentRanking()
    {
        List<dreamloLeaderBoard.Score> highscore = dreamlo.ToListHighToLow();
        return highscore.Find(score => score.playerName == playerName && score.score == gameScore);
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
            Debug.LogWarning("Dreamlo: Previous Highscore List could not be loaded");
        }
    }

}

