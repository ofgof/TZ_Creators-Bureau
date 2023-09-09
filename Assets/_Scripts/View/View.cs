using System;
using TMPro;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _coinText;
    [SerializeField] protected TextMeshProUGUI _levelText;


    public virtual void Init(Action openMainMenu)
    {
        Close();
        GameEvents.OnMoneyChange += UpdateUI;
    }
    protected virtual void OnDestroy()
    {
        GameEvents.OnMoneyChange -= UpdateUI;
    }
    protected virtual void UpdateUI()
    {
        var data = GameManager.Instance.DataManager.Data;
        _coinText.text = data.Money.ToString();
        _levelText.text = data.Level.ToString();
    }
    public virtual void Open()
    {
        UpdateUI();
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
