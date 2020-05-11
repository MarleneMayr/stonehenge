using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreState : State
{
    [SerializeField] private GameObject playground;

    public override void AfterActivate()
    {

    }

    public override void BeforeDeactivate()
    {

    }

    public override void OnTrackerLost()
    {
        playground.SetActive(false);
    }

    public override void OnTrackerFound()
    {
        playground.SetActive(true);
    }
}
