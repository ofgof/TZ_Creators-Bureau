using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private ConfigHolder _configHolder;

    [SerializeField] private DataManager _dataManager;
    [SerializeField] private InputController _inputController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private BulletController _bulletController;
    [SerializeField] private LevelController _levelController;
    [SerializeField] private ViewController _viewController;

    public ConfigHolder ConfigHolder => _configHolder;
    public DataManager DataManager => _dataManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _levelController.Init();

        _dataManager.Init();

        //_inputController.Init();
        _playerController.Init();

        _bulletController.Init();
        _viewController.Init();

    }

}
