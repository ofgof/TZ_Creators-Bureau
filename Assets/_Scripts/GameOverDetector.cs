using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var character = other.gameObject.GetComponent<CharacterController>();
        if(character != null)
        {
            Debug.Log($"[{name}] Game over you fall down");
            GameEvents.OnGameFail?.Invoke();
        }
    }
}
