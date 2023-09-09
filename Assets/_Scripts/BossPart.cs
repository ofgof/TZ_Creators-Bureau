using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPart : MonoBehaviour
{
    [SerializeField] private EnemyBoss _enemyBoss;
    private void OnTriggerEnter(Collider other)
    {
        var character = other.gameObject.GetComponent<CharacterController>();
        if (character != null)
        {
            Debug.Log($"[{name}] Start boss fight");
            character.MoveToCenter().OnComplete(() =>
            {
                _enemyBoss.Init();
                _enemyBoss.Move(character.transform);
                GameEvents.OnBossFight?.Invoke();
            });
            
        }
    }
}
