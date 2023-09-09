using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _sideSpeed;

    private Sequence _sideSequence;

    private Vector3 _resetPosition;

    public void Init()
    {
        _resetPosition = transform.position;
        GameEvents.OnGameStart += () =>
        {
            MoveForward();
        };
        GameEvents.OnGameFail += StopMove;
        GameEvents.OnBossFight += StopMove;
        GameEvents.OnGameRestart += ResetCharacter;
        InputController.OnSwipe += MoveSide;
    }
    private void OnDestroy()
    {
        GameEvents.OnGameStart -= () =>
        {
            MoveForward();
        };
        GameEvents.OnGameFail -= StopMove;
        GameEvents.OnBossFight -= StopMove;
        GameEvents.OnGameRestart -= ResetCharacter;
        InputController.OnSwipe -= MoveSide;
    }
    public Tween MoveToCenter()
    {
        transform.DOKill();
        return transform.DOMoveX(0f, 0.5f).SetEase(Ease.Linear);
    }
    private void MoveSide(float direction)
    {
        _sideSequence.Kill();
        _sideSequence = DOTween.Sequence();
        var position = transform.position;
        var deltaPosition = direction * _sideSpeed * Time.deltaTime * Vector3.right;
        var targetPosition = position + deltaPosition;


        _sideSequence.Append(transform.DOMoveX(targetPosition.x, Time.deltaTime));
    }
    private void MoveForward()
    {
        float duration = 1f;
        var targetPosition = transform.position + _forwardSpeed * Vector3.forward;
        transform.DOMoveZ(targetPosition.z, duration).SetEase(Ease.Linear).OnComplete(MoveForward);
    }
    private void StopMove()
    {
        transform.DOKill();
    }
    private void ResetCharacter()
    {
        StopMove();
        transform.position = _resetPosition;
    }
}
