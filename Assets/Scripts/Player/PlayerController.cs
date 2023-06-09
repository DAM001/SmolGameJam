using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterMovement _characterMovement;

    private void OnMovement(InputValue inputValue)
    {
        _characterMovement.Move(inputValue.Get<Vector2>());
    }

    private void OnAimMouse(InputValue inputValue)
    {

    }
}
