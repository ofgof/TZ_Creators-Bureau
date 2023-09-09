using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField] private MainView _mainView;
    [SerializeField] private GameView _gameView;
    [SerializeField] private FailView _failView;
    [SerializeField ]private WinView _winView;

    public void Init()
    {
        _mainView.Init();
        _gameView.Init(OpenMainView);
        _failView.Init(OpenMainView);
        _winView.Init(OpenMainView);

        _mainView.Open();

        GameEvents.OnGameStart += OpenGameView;
        GameEvents.OnGameFail += OpenFailView;
        GameEvents.OnGameWin += OpenWinView;
    }
    private void OnDestroy()
    {
        GameEvents.OnGameStart -= OpenGameView;
        GameEvents.OnGameFail -= OpenFailView;
        GameEvents.OnGameWin += OpenWinView;
    }
    private void OpenMainView()
    {
        CloseAllViews();
        _mainView.Open();
    }
    private void OpenGameView()
    {
        CloseAllViews();
        _gameView.Open();
    }
    private void OpenFailView()
    {
        CloseAllViews();
        _failView.Open();
    }
    private void OpenWinView()
    {
        CloseAllViews();
        _winView.Open();
    }
    private void CloseAllViews()
    {
        _mainView.Close();
        _gameView.Close();
        _failView.Close();
        _winView.Close();
    }
}
