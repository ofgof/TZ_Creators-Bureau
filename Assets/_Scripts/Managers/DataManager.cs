using JetBrains.Annotations;
using System;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

[Serializable]
public class SaveModel
{
    public float money;
    public int level;
    public int weaponLevel;
    public int bulletSpeedLevel;
    public int bulletDamageLevel;

    public SaveModel()
    {
        money = 0;
        level = 1;
        weaponLevel = 0;
        bulletSpeedLevel = 0;
        bulletDamageLevel = 0;
    }
    public SaveModel(Data data)
    {
        money = data.Money;
        level = data.Level;
        weaponLevel = data.WeaponLevel;
        bulletSpeedLevel = data.BulletSpeedLevel;
        bulletDamageLevel = data.BulletDamageLevel;
    }
}

[Serializable]
public class Data
{
    [SerializeField] private float _money;

    [SerializeField] private int _level; 

    [SerializeField] private int _weaponLevel;
    [SerializeField] private int _bulletSpeedLevel;
    [SerializeField] private int _bulletDamageLevel;

    public float Money => _money;
    public int Level => _level;
    public int WeaponLevel => _weaponLevel;
    public int BulletSpeedLevel => _bulletSpeedLevel;
    public int BulletDamageLevel => _bulletDamageLevel;

    public Data(SaveModel save)
    {
        _money = save.money;
        _level = save.level;
        _weaponLevel = save.weaponLevel;
        _bulletSpeedLevel = save.bulletSpeedLevel;
        _bulletDamageLevel = save.bulletDamageLevel;
    }
    public void Init()
    {
        GameEvents.OnEnemyKilled += AddMoney;
        GameEvents.UpShootFrequence += AddWeaponLevel;
        GameEvents.UpBulletSpeed += AddBulletSpeedLevel;
        GameEvents.UpBulletDamage += AddBulletDamageLevel;
        GameEvents.OnGameWin += LevelUp;
    }
    public void Destroy()
    {
        GameEvents.OnEnemyKilled -= AddMoney;
        GameEvents.UpShootFrequence -= AddWeaponLevel;
        GameEvents.UpBulletSpeed -= AddBulletSpeedLevel;
        GameEvents.UpBulletDamage -= AddBulletDamageLevel;
        GameEvents.OnGameWin -= LevelUp;
    }
    private void AddMoney(float amount)
    {
        _money += Mathf.Abs(amount);
        GameEvents.OnMoneyChange?.Invoke();
        GameEvents.OnSave?.Invoke();
    }
    public bool WriteOffMoney(float amount)
    {
        amount = Mathf.Abs(amount);

        if (_money < amount) return false;

        _money -= amount;
        GameEvents.OnMoneyChange?.Invoke();
        GameEvents.OnSave?.Invoke();
        return true;
    }
    private void AddWeaponLevel()
    {
        _weaponLevel++;
        GameEvents.OnSave?.Invoke();
    }
    private void AddBulletSpeedLevel()
    {
        _bulletSpeedLevel++;
        GameEvents.OnSave?.Invoke();
    }
    private void AddBulletDamageLevel()
    {
        _bulletDamageLevel++;
        GameEvents.OnSave?.Invoke();
    }
    private void LevelUp()
    {
        _level++;
        GameEvents.OnSave?.Invoke();
    }
}

public class DataManager : MonoBehaviour
{
    private const string DATA_KEY = "PlayerSave";

    [SerializeField] private Data _data;
    public Data Data => _data;
    public void Init()
    {
        _data = Load();
        _data.Init();

        GameEvents.OnSave += Save;
    }
    private void OnDestroy()
    {
        _data.Destroy();
        GameEvents.OnSave -= Save;
    }
    private void Save()
    {
        var json = JsonUtility.ToJson(new SaveModel(_data));
        PlayerPrefs.SetString(DATA_KEY, json);
    }
    private Data Load()
    {
        if(PlayerPrefs.HasKey(DATA_KEY))
        {
            var save = JsonUtility.FromJson<SaveModel>(PlayerPrefs.GetString(DATA_KEY));
            return new Data(save);
        }
        return new Data(new SaveModel());
    }
}
