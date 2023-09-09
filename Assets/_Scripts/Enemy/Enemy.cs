using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected EnemyConfig _config;
    [SerializeField] protected EnemyUI _enemyUI;

    [SerializeField] protected int _health;  

    public virtual void Init()
    {
        _config = GameManager.Instance.ConfigHolder.enemyConfig;
        _health = _config.health;

        _enemyUI.UpdateUI(_health);
        _enemyUI.gameObject.SetActive(true);
        GameEvents.OnGameRestart += ResetEnemy;
    }
    protected void OnDestroy()
    {
        GameEvents.OnGameRestart -= ResetEnemy;
    }
    public virtual void GetDamage(int damage)
    {
        //play blood vfx
        _health -= damage;
        _enemyUI.UpdateUI(_health);
        if (_health <= 0 )
        {
            Kill();
        }
        Debug.Log($"[{name} i got damege]");
    }
    protected virtual void Kill()
    {
        transform.DOKill();
        GameEvents.OnEnemyKilled?.Invoke(_config.value);
        Destroy(gameObject);
    }
    protected virtual void ResetEnemy()
    {
        Destroy(gameObject);
    }
}
