using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [Header("Properties:")]
    [SerializeField] private float _moveSpeed = 1f;

    private Vector3 _moveDirection = Vector3.zero;

    public void Move(Vector2 direction)
    {
        _moveDirection = new Vector3(direction.x, 0f, direction.y) * _moveSpeed;
    }

    private void Update()
    {
        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}
