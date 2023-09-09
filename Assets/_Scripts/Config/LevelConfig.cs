using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public float distanceBetweenEnemy = 5;
    public int minLevelLength = 100;
    public int maxLevelLength = 300;
}
