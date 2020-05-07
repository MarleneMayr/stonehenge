using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreState : State
{

    public override void AfterActivate()
    {
        Debug.Log("Highscorestate activated");
    }

    public override void BeforeDeactivate()
    {

    }
}
