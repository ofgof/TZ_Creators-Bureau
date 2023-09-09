using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private BulletConfig _config;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnPosition;

    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _bulletDamage;

    private int _speedLevel;
    private int _damageLevel;

    public void Init()
    {
        var data = GameManager.Instance.DataManager.Data;
        _speedLevel = data.BulletSpeedLevel;
        _damageLevel = data.BulletSpeedLevel;

        _bulletSpeed = _config.defaultSpeed + _speedLevel * _config.increaseSpeedPerLevel;
        _bulletDamage = _config.defaultDamage + _damageLevel * _config.increaseDamagePerLevel;

        GameEvents.OnShoot += SpawnBullet;

        GameEvents.OnUpgradeBulletSpeed += UpgradeBulletSpeed;
        GameEvents.OnUpgradeBulletDamage += UpgradeBulletDamage;
    }
    private void OnDestroy()
    {
        GameEvents.OnShoot -= SpawnBullet;

        GameEvents.OnUpgradeBulletSpeed -= UpgradeBulletSpeed;
        GameEvents.OnUpgradeBulletDamage -= UpgradeBulletDamage;
    }

    private void SpawnBullet()
    {
        var bullet = Instantiate(_bulletPrefab, transform);
        bullet.transform.position = _spawnPosition.position;

        bullet.GetComponent<Bullet>().Init(_bulletSpeed, _bulletDamage);
    }
    private void UpgradeBulletSpeed()
    {
        if (_speedLevel >= _config.maxSpeedLevel) return;
        if (GameManager.Instance.DataManager.Data.WriteOffMoney(_config.speedUpgradePrice))
        {
            _bulletSpeed += _config.increaseSpeedPerLevel;
            _speedLevel++;
            GameEvents.UpBulletSpeed?.Invoke();
        }
            
    }
    private void UpgradeBulletDamage()
    {
        if (_damageLevel >= _config.maxDamageLevel) return;
        if (GameManager.Instance.DataManager.Data.WriteOffMoney(_config.damageUpgradePrice))
        {
            _bulletDamage += _config.increaseDamagePerLevel;
            _damageLevel++;

            GameEvents.UpBulletDamage?.Invoke();
        }            
    }
}
