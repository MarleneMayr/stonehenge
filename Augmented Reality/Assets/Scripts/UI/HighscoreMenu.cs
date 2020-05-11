using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HighscoreMenu : Menu
{
    [Header("Colors")]
    public Color yellow;
    public Color green;
    public Color red;

    [Space]
    [Header("UI Objects")]
    public TMP_Text scoreTxt;
    public GameObject onlinePanel;

    public TMP_InputField nameInput;
    public GameObject enterNamePanel;

    [Header("Previous Highscore")]
    public GameObject previousHighscorePanel;
    public TMP_Text previousDateTxt;
    public TMP_Text previousScoreTxt;
    public TMP_Text previousDifferenceTxt;

    [Header("Leaderboard")]
    public GameObject highscorePanel;
    public GameObject ownHighscorePanel;
    public HighscoreUI highscorePrefab;

    [Space]
    [Header("Events")]
    public UnityEvent<string> OnSubmitName;
    public UnityEvent OnPlayAgainPressed;

    public void SubmitName()
    {
        string name = nameInput.text;
        if (nameInput.text.Length == 0)
        {
            nameInput.placeholder.GetComponent<Text>().text = "Enter name here...";
        }
        else
        {
            OnSubmitName?.Invoke(name);
            enterNamePanel.SetActive(false);
        }
    }

    public void ShowScore(int score)
    {
        scoreTxt.SetText(score.ToString());
    }

    public void ShowOnlineHighscores(List<dreamloLeaderBoard.Score> scores, dreamloLeaderBoard.Score currentScore, dreamloLeaderBoard.Score previousScore)
    {
        enterNamePanel.SetActive(false);
        onlinePanel.SetActive(true);

        ShowPreviousHighscore(previousScore, currentScore.score);

        ClearHighscoresFromPanel(highscorePanel);
        bool isInTop5 = false;
        foreach (var score in scores)
        {
            var highscore = Instantiate(highscorePrefab, highscorePanel.transform);
            highscore.SetValues(score);
            if (currentScore.id == score.id)
            {
                isInTop5 = true;
                highscore.SetColor(yellow);
            }
        }

        if (isInTop5)
        {
            ownHighscorePanel.SetActive(false);
        }
        else
        {
            ownHighscorePanel.SetActive(true);
            ClearHighscoresFromPanel(ownHighscorePanel);
            var highscore = Instantiate(highscorePrefab, ownHighscorePanel.transform);
            highscore.SetValues(currentScore);
            highscore.SetColor(yellow);
        }
    }

    private void ClearHighscoresFromPanel(GameObject panel)
    {
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void ShowPreviousHighscore(dreamloLeaderBoard.Score previousScore, int currentScore)
    {
        previousHighscorePanel.SetActive(true);

        previousDateTxt.SetText(previousScore.dateString);
        previousScoreTxt.SetText(previousScore.score.ToString());

        int difference = currentScore - previousScore.score;
        previousDifferenceTxt.SetText(string.Format("{0,6}", difference.ToString("+#;-#;0")));
        if (difference > 0)
        {
            previousDifferenceTxt.color = green;
        }
        else if (difference < 0)
        {
            previousDifferenceTxt.color = red;
        }
        else
        {
            previousDifferenceTxt.color = Color.white;
        }
    }

    public void ShowNamePanel(string name = null)
    {
        onlinePanel.SetActive(false);
        previousHighscorePanel.SetActive(false);

        enterNamePanel.SetActive(true);

        if (name != null)
        {
            nameInput.text = name;
        }
        else
        {
            nameInput.text = string.Empty;
            nameInput.placeholder.GetComponent<Text>().text = "Name...";
        }
    }

    public void PlayAgain()
    {
        OnPlayAgainPressed?.Invoke();
    }
}
