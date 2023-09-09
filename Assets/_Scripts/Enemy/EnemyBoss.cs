using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    [SerializeField] private bool _isInited;

    private Vector3 _startPosition;

    public override void Init()
    {
        _config = GameManager.Instance.ConfigHolder.enemyBossConfig;
        _startPosition = transform.localPosition;
        _health = _config.health * GameManager.Instance.DataManager.Data.Level;

        _enemyUI.UpdateUI(_health);
        _enemyUI.gameObject.SetActive(true);
        GameEvents.OnGameRestart += ResetEnemy;
        _isInited = true;
    }
    public override void GetDamage(int damage)
    {
        if (!_isInited) return;
        base.GetDamage(damage);
    }
    protected override void Kill()
    {
        GameEvents.OnGameWin?.Invoke();
        transform.DOKill();
        GameEvents.OnEnemyKilled?.Invoke(_config.value * GameManager.Instance.DataManager.Data.Level);
        gameObject.SetActive(false);
    }
    protected override void ResetEnemy()
    {
        _enemyUI.gameObject.SetActive(false);
        transform.localPosition = _startPosition;
        _isInited = false;
        gameObject.SetActive(true);
    }
    public void Move(Transform target)
    {
        var duration = (target.position - transform.position).magnitude / _config.speed;
        transform.DOMove(target.position, duration).SetEase(Ease.Linear);
    }
}
