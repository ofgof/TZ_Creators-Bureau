using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static Action<float> OnSwipe;

    [SerializeField] float _maxMagnitude = 200f;

    private float _touchPositionX;
    private bool _canControll = false;

    private void Start()
    {
        GameEvents.OnGameStart += Init;
        GameEvents.OnBossFight += () =>
        {
            _canControll = false;
        };
    }
    public void Init()
    {
        _canControll = true;
    }
    private void OnDestroy()
    {
        GameEvents.OnGameStart -= Init;
        GameEvents.OnBossFight -= () =>
        {
            _canControll = false;
        };
    }

    private void Update()
    {
        if (!_canControll) return;

        if(Input.GetMouseButtonDown(0))
        {
            _touchPositionX = GetTouchPosition().x;
        }
        if(Input.GetMouseButton(0))
        {
            var curentTouchPositionX = GetTouchPosition().x;
            var magnitude = curentTouchPositionX - _touchPositionX;
            _touchPositionX = curentTouchPositionX;
            if (Mathf.Abs(magnitude) > _maxMagnitude)
            {
                magnitude = _maxMagnitude * Mathf.Sign(magnitude);
            }
            OnSwipe?.Invoke(magnitude);
        }
    }
    private Vector2 GetTouchPosition()
    {
#if UNITY_EDITOR
        return Input.mousePosition;
#else
        return Input.touches[0].position;
#endif
    }
}
