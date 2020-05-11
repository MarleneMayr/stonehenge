using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class CountdownMenu : Menu
{
    [SerializeField] private TextMeshProUGUI countdownTxt;

    public bool isRunning = false;

    public void StartCountdown(Action onComplete)
    {
        StartCoroutine(Countdown(onComplete));
    }

    private IEnumerator Countdown(Action onComplete)
    {
        isRunning = true;
        float speed = 0.5f;

        countdownTxt.SetText("3");
        Show(speed);
        yield return new WaitForSeconds(speed);
        Hide(speed);
        yield return new WaitForSeconds(speed);

        countdownTxt.SetText("2");
        Show(speed);
        yield return new WaitForSeconds(speed);
        Hide(speed);
        yield return new WaitForSeconds(speed);

        countdownTxt.SetText("1");
        Show(speed);
        yield return new WaitForSeconds(speed);
        Hide(speed);
        yield return new WaitForSeconds(speed);

        countdownTxt.SetText("Go!");
        Show(speed);
        yield return new WaitForSeconds(speed);

        onComplete();
        isRunning = false;

        Hide(speed);
    }
}