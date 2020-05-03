using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : State
{
    [SerializeField] private GameMenu gameMenu;

    public override void Activate()
    {
        gameMenu.StartCountdown(() => StartTimer());
    }

    public override void Deactivate()
    {
        gameMenu.Hide();
    }

    private void StartTimer()
    {
        StartCoroutine(Timer(time: 5f, () => EndGame()));
    }

    private IEnumerator Timer(float time, Action onCompleted)
    {
        while (time > 0)
        {
            gameMenu.SetTimerTxt($"{time}");
            yield return new WaitForSeconds(1);
            time--;
        }
        onCompleted();
    }

    private void EndGame()
    {
        stateMachine.GoTo<HighscoreState>();
    }
}
