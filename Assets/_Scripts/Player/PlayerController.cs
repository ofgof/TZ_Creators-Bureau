using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _weaponPosition;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Weapon _weapon;

    public void Init()
    {
        _characterController.Init();
        _weapon.Init();
    }
}
