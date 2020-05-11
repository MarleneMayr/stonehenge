using UnityEngine;
using System.Collections.Generic;

public class Highscore : MonoBehaviour
{

    public int totalScore = 0;
    public string playerName = "";
    public string code = "";

    // Reference to the dreamloLeaderboard prefab in the scene	
    dreamloLeaderBoard dreamlo;

    void Start()
    {
        dreamlo = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
    }

    public void UploadScore()
    {
        if (dreamlo.publicCode == "") Debug.LogError("Dreamlo: publicCode missing");
        if (dreamlo.privateCode == "") Debug.LogError("Dreamlo: privateCode missing");

        dreamlo.AddScore(playerName, totalScore);
    }

    public List<dreamloLeaderBoard.Score> GetTopFive()
    {
        List<dreamloLeaderBoard.Score> highscore = dreamlo.ToListHighToLow();
        return highscore?.GetRange(0, 5);

        //foreach (dreamloLeaderBoard.Score currentScore in top5)
        //{
        //    // display score
        //}
    }

    public dreamloLeaderBoard.Score GetCurrentRanking()
    {
        List<dreamloLeaderBoard.Score> highscore = dreamlo.ToListHighToLow();
        return highscore.Find(score => score.playerName == playerName && score.score == totalScore);
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

