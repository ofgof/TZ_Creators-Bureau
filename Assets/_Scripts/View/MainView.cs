using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class UpgradeButton
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _levelText;

    public void Init(Action onClick, Action onUpdateUI)
    {
        onUpdateUI();
        _button.onClick.AddListener(() =>
        {
            onClick();
            onUpdateUI();
        });
    }
    public void SetPrice(float price)
    {
        _priceText.text = price.ToString();
    }
    public void SetLevel(float level)
    {
        _levelText.text = level.ToString();
    }
}

public class MainView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _levelText;

    [SerializeField] private UpgradeButton _upSpeedButton;
    [SerializeField] private UpgradeButton _upDamageButton;
    [SerializeField] private UpgradeButton _upFrequenceButton;

    [SerializeField] private Button _play;

    public void Init()
    {
        InitButtons();
        //UpdateUI();
        Close();

        GameEvents.OnMoneyChange += UpdateUI;
    }
    private void OnDestroy()
    {
        GameEvents.OnMoneyChange -= UpdateUI;
    }
    private void InitButtons()
    {
        var configHolder = GameManager.Instance.ConfigHolder;
        var data = GameManager.Instance.DataManager.Data;
        _play.onClick.AddListener(() =>
        {
            Close();
            GameEvents.OnGameStart?.Invoke();
        });
        _upSpeedButton.Init(
            () =>
            {
                GameEvents.OnUpgradeBulletSpeed?.Invoke();
            },
            () =>
            {
                _upSpeedButton.SetPrice(configHolder.bulletConfig.speedUpgradePrice);
                _upSpeedButton.SetLevel(data.BulletSpeedLevel);
            });
        _upDamageButton.Init(
            () =>
            {
                GameEvents.OnUpgradeBulletDamage?.Invoke();
            },
            () =>
            {
                _upDamageButton.SetPrice(configHolder.bulletConfig.damageUpgradePrice);
                _upDamageButton.SetLevel(data.BulletDamageLevel);
            });
        _upFrequenceButton.Init(
            () =>
            {
                GameEvents.OnUpgradeShootFrequence?.Invoke();
            },
            () =>
            {
                _upFrequenceButton.SetPrice(configHolder.weaponConfig.upgradePrice);
                _upFrequenceButton.SetLevel(data.WeaponLevel);
            });
    }
    private void UpdateUI()
    {
        var data = GameManager.Instance.DataManager.Data;
        _coinText.text = data.Money.ToString();
        _levelText.text = data.Level.ToString();
    }
    public void Open()
    {
        UpdateUI();
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
