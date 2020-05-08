using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameMenu : Menu
{
    [SerializeField] private TextMeshProUGUI hudTxt;
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private TextMeshProUGUI scoreTxt;

    public bool isCountdownOn = false;
    public UnityEvent ScreenTapped;

    public void Tap()
    {
        ScreenTapped.Invoke();
    }

    public void StartCountdown(Action onComplete)
    {
        StartCoroutine(Countdown(onComplete));
    }

    private IEnumerator Countdown(Action onComplete)
    {
        isCountdownOn = true;
        float speed = 0.5f;
        hudTxt.gameObject.SetActive(true);

        hudTxt.SetText("3");
        Show(speed);
        yield return new WaitForSeconds(speed);
        Hide(speed);
        yield return new WaitForSeconds(speed);

        hudTxt.SetText("2");
        Show(speed);
        yield return new WaitForSeconds(speed);
        Hide(speed);
        yield return new WaitForSeconds(speed);

        hudTxt.SetText("1");
        Show(speed);
        yield return new WaitForSeconds(speed);
        Hide(speed);
        yield return new WaitForSeconds(speed);

        hudTxt.SetText("Go!");
        Show(speed);
        yield return new WaitForSeconds(speed);

        onComplete();

        yield return new WaitForSeconds(2 * speed);
        hudTxt.gameObject.SetActive(false);
        isCountdownOn = false;
    }

    public void SetTimerTxt(string text)
    {
        timerTxt.SetText(text);
    }

    public void SetScoreTxt(int score)
    {
        scoreTxt.SetText("score " + score);
    }
}
