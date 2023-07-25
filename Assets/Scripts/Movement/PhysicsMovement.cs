using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MovementBase
{
    [SerializeField] private Rigidbody _rigidbody;

    private void Update()
    {
        _rigidbody.AddForce(_moveDirection * _moveForce * Time.fixedDeltaTime);
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
