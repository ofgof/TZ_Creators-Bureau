using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public float defaultShootFrequency;
    public float decreaseShootFrequencyPerLevel;
    public int maxLevelShootFrequency;
    public int upgradePrice;
}
