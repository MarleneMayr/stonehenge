using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameMenu : Menu
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private Image timerIcon;
    [SerializeField] private Color colorRegular;
    [SerializeField] private Color colorWarning;

    private bool isWarning = false;
    public UnityEvent ScreenTapped;

    public void Tap()
    {
        ScreenTapped?.Invoke();
    }

    public void SetScoreTxt(int score)
    {
        scoreTxt.SetText(score.ToString());
    }

    public void SetTimerTxt(int text)
    {
        timerTxt.SetText(text.ToString());
    }

    public void SetTimerWarning(bool turnWarningColorOn)
    {
        if (turnWarningColorOn != isWarning)
        {
            isWarning = turnWarningColorOn;
            timerTxt.color = isWarning ? colorWarning : colorRegular;
            timerIcon.color = isWarning ? colorWarning : colorRegular;
        }
    }
}
