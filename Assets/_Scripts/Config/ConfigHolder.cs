using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/ConfigHolder")]
public class ConfigHolder : ScriptableObject
{
    public BulletConfig bulletConfig;
    public EnemyConfig enemyConfig;
    public EnemyConfig enemyBossConfig;
    public WeaponConfig weaponConfig;
}
