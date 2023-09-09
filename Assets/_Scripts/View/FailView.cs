using System;
using UnityEngine;
using UnityEngine.UI;

public class FailView : View
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
