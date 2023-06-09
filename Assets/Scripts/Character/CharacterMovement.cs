using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [Header("Properties:")]
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _minDistance = .5f;

    private Vector3 _moveDirection = Vector3.zero;

    public void Move(Vector2 direction)
    {
        _moveDirection = new Vector3(direction.x, 0f, direction.y) * _moveSpeed;
    }

    public void LookAt(Vector3 pos)
    {
        if (Vector3.Distance(transform.position, pos) < _minDistance) return;
        Quaternion targetRot = Quaternion.LookRotation(pos - transform.position, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, _rotationSpeed * Time.deltaTime);
    }

    private void Update()
    {
        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}
