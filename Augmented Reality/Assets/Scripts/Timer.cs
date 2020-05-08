using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float Duration;
    public float TimeLeft;
    public bool isRunning = false;

    public void StartTimer(float duration, Action<string> printTime, Action onCompleted)
    {
        Duration = duration;
        TimeLeft = duration;
        StartCoroutine(RunTimer(printTime, onCompleted));
    }

    public void AddToTimer(float duration)
    {
        Duration += duration;
        TimeLeft += duration;
    }

    private IEnumerator RunTimer(Action<string> printTime, Action onCompleted)
    {
        isRunning = true;
        while (TimeLeft > 0)
        {
            printTime($"{TimeLeft}");
            yield return new WaitForSeconds(1);
            TimeLeft--;
        }
        onCompleted();
        isRunning = false;
    }
}
