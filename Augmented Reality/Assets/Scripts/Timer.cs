using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public int Duration;
    public int TimeLeft;
    public bool isRunning = false;

    [Serializable] public class TimerEvent : UnityEvent<int> { }
    public TimerEvent OnTimerTick;

    public void StartTimer(int duration, Action onCompleted)
    {
        Duration = duration;
        TimeLeft = duration;
        StartCoroutine(RunTimer(onCompleted));
    }

    public int AddToTimer(int duration)
    {
        Duration += duration;
        TimeLeft += duration;
        return TimeLeft;
    }

    private IEnumerator RunTimer(Action onCompleted)
    {
        isRunning = true;
        while (TimeLeft > 0)
        {
            OnTimerTick?.Invoke(TimeLeft);
            yield return new WaitForSeconds(1);
            TimeLeft--;
        }
        onCompleted();
        isRunning = false;
    }
}
