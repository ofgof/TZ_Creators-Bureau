using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;

    public void UpdateUI(int health)
    {
        _healthText.text = health.ToString();
    }
}
