using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BulletConfig")]
public class BulletConfig : ScriptableObject
{
    [Header("Bullet Speed")]
    public float defaultSpeed;
    public float increaseSpeedPerLevel;
    public int maxSpeedLevel;
    public int speedUpgradePrice;

    [Header("Bullet Damage")]
    public int defaultDamage;
    public int increaseDamagePerLevel;
    public int maxDamageLevel;
    public int damageUpgradePrice;
}
