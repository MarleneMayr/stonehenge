using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMenu : Menu
{
    [SerializeField] private TextMeshProUGUI hudTxt;
    [SerializeField] private TextMeshProUGUI timerTxt;

    public void StartCountdown(Action onComplete)
    {
        StartCoroutine(Countdown(onComplete));
    }

    private IEnumerator Countdown(Action onComplete)
    {
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
    }

    internal void SetTimerTxt(string text)
    {
        timerTxt.SetText(text);
    }
}
