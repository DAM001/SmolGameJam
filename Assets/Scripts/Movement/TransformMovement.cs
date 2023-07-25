using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransformMovement : MovementBase
{
    [SerializeField] protected float _acceleration = 10f;

    protected Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        AcceleratedMovement();
    }

    protected virtual void AcceleratedMovement()
    {
        Vector3 targetVelocity = _moveDirection;
        _velocity = Vector3.Lerp(_velocity, targetVelocity, _acceleration * Time.deltaTime);

        Debug.Log(_velocity.normalized);
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * .5f, _velocity, out hit, _velocity.normalized.magnitude * .5f))
        {

        }
        else
        {
            transform.position += (_velocity * _moveSpeed * Time.deltaTime);
        }
    }
}
