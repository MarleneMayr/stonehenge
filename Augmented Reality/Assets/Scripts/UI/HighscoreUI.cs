using System;
using TMPro;
using UnityEngine;

public class HighscoreUI : MonoBehaviour
{
    public TMP_Text PositionTxt;
    public TMP_Text ScoreTxt;
    public TMP_Text NameTxt;
    public TMP_Text DateTxt;

    public void SetValues(dreamloLeaderBoard.Score score)
    {
        PositionTxt.SetText(String.Format("{0,4}.", score.id));
        ScoreTxt.SetText(String.Format("{0,6}", score.score));
        NameTxt.SetText(score.playerName);
        DateTxt.SetText(score.dateString);
    }

    public void SetColor(Color color)
    {
        PositionTxt.color = color;
        ScoreTxt.color = color;
        NameTxt.color = color;
        DateTxt.color = color;
    }
}
