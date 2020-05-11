using UnityEngine;
using System.Collections.Generic;

public class Highscore : MonoBehaviour
{
    public int gameScore = 0;
    public string playerName = "";

    dreamloLeaderBoard dreamlo;

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

    public dreamloLeaderBoard.Score GetPersonalBest()
    {
        dreamlo.GetScores();
        List<dreamloLeaderBoard.Score> highscore = dreamlo.ToListHighToLow();
        return highscore.Find(score => score.playerName == playerName);

        /*
         * // async TODO methode public async machen
        bool highscoreAvailable = dreamlo.GetScores();

        if (highscoreAvailable) 
        {
            List<dreamloLeaderBoard.Score> highscore = dreamlo.ToListHighToLow();
            return highscore.Find(score => score.playerName == playerName);
        }
        else
        {
            Debug.LogWarning("Dreamlo: Previous Highscore could not be loaded");
        }
        */
    }

}

