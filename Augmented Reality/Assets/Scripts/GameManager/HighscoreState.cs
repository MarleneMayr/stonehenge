using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreState : State
{
    [SerializeField] private HighscoreMenu highscoreMenu;

    public override void Activate()
    {
        highscoreMenu.Show();
    }

    public override void Deactivate()
    {
        highscoreMenu.Hide();
    }
}
