using System;
using System.Collections;
using UnityEngine;

public class GameState : State
{
    [SerializeField] private GameMenu gameMenu;
    [SerializeField] private MoldChecker moldChecker;
    [SerializeField] private Cookbook cookbook;

    private int score = 0;

    public override void Activate()
    {
        gameMenu.StartCountdown(() => StartTimer());

        NextRecipe();
        moldChecker.OnMoldMatch.AddListener(NextRecipe);
        moldChecker.OnMoldMatch.AddListener(UpdateScore);
    }

    public override void Deactivate()
    {
        gameMenu.Hide();
        moldChecker.StopChecking();
        moldChecker.OnMoldMatch.RemoveListener(NextRecipe);
        moldChecker.OnMoldMatch.RemoveListener(UpdateScore);
    }

    private void NextRecipe()
    {
        Recipe next = cookbook.GetNext();
        //print("------------------------\nRECIPE: ");
        //foreach (var brick in next.Ingredients)
        //{
        //    print(brick.ToString());
        //}
        moldChecker.StartChecking(next);
        print("start next");
    }

    private void UpdateScore()
    {
        score++;
        gameMenu.SetScoreTxt(score);
    }

    private void StartTimer()
    {
        StartCoroutine(Timer(time: 60f, () => EndGame()));
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
