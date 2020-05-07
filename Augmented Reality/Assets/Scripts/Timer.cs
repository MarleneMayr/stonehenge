using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float Duration { get; set; }
    public float CurrentTime { get; set; }
    public bool isRunning = false;

    public void StartTimer(float duration, Action<string> printTime, Action onCompleted)
    {
        Duration = duration;
        CurrentTime = duration;
        StartCoroutine(RunTimer(printTime, onCompleted));
    }

    public void AddToTimer(float duration)
    {
        Duration += duration;
        CurrentTime += duration;
    }

    private IEnumerator RunTimer(Action<string> printTime, Action onCompleted)
    {
        isRunning = true;
        while (CurrentTime > 0)
        {
            printTime($"{CurrentTime}");
            yield return new WaitForSeconds(1);
            CurrentTime--;
        }
        onCompleted();
        isRunning = false;
    }
}
