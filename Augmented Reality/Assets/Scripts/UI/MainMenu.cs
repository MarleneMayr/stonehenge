using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenu : Menu
{
    public UnityEvent OnStartClicked;

    public void StartGame()
    {
        OnStartClicked.Invoke();
    }
}
