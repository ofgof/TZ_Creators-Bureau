using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : View
{
    [SerializeField] private Button _mainMenuButton;

    public override void Init(Action openMainMenu)
    {
        _mainMenuButton.onClick.AddListener(() =>
        {
            openMainMenu();
            GameEvents.OnGameRestart?.Invoke();
        });
        base.Init(openMainMenu);       
    }
}
