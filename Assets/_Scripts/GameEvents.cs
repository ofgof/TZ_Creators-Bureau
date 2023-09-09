using System;
using UnityEngine;

public class GameEvents
{
    public static Action OnGameStart;
    public static Action OnGameRestart;
    public static Action OnGameFail;
    public static Action OnGameWin;
    public static Action OnBossFight;

    public static Action OnShoot;
    public static Action<float> OnEnemyKilled;

    public static Action OnUpgradeShootFrequence;
    public static Action OnUpgradeBulletSpeed;
    public static Action OnUpgradeBulletDamage;

    public static Action UpShootFrequence;
    public static Action UpBulletSpeed;
    public static Action UpBulletDamage;

    public static Action OnMoneyChange;

    public static Action OnSave;
}
