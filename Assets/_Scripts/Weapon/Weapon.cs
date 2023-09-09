using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponConfig _config;

    [SerializeField] private float _shootFrequency;
    [SerializeField] private int _frequencyLevel;

    private Coroutine _shootCoroutine;
    public void Init()
    {
        _frequencyLevel = GameManager.Instance.DataManager.Data.WeaponLevel;
        _shootFrequency = _config.defaultShootFrequency - _frequencyLevel * _config.decreaseShootFrequencyPerLevel;

        GameEvents.OnGameStart += () =>
        {
            _shootCoroutine = StartCoroutine(Shoot());
        };
        GameEvents.OnGameFail += StopShoot;
        GameEvents.OnGameWin += StopShoot;
        GameEvents.OnGameRestart += ResetWeapon;
        GameEvents.OnUpgradeShootFrequence += UpgradeShootFrequency;
    }
    private void OnDestroy()
    {
        GameEvents.OnGameStart -= () =>
        {
            _shootCoroutine = StartCoroutine(Shoot());
        };
        GameEvents.OnGameFail -= StopShoot;
        GameEvents.OnGameWin -= StopShoot;
        GameEvents.OnGameRestart -= ResetWeapon;
        GameEvents.OnUpgradeShootFrequence -= UpgradeShootFrequency;
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            GameEvents.OnShoot?.Invoke();

            yield return new WaitForSeconds(_shootFrequency);
        }
    }

    private void UpgradeShootFrequency()
    {
        if (_frequencyLevel >= _config.maxLevelShootFrequency) return;
        if(GameManager.Instance.DataManager.Data.WriteOffMoney(_config.upgradePrice))
        {
            _shootFrequency -= _config.decreaseShootFrequencyPerLevel;

            _frequencyLevel++;
            GameEvents.UpShootFrequence?.Invoke();
        }
    }
    private void StopShoot()
    {
        if (_shootCoroutine != null)
            StopCoroutine(_shootCoroutine);
    }
    private void ResetWeapon()
    {
        StopShoot();
    }
}
