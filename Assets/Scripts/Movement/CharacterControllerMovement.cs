using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MovementBase
{
    [SerializeField] protected CharacterController _characterController;
    [Header("Properties:")]
    [SerializeField] protected float _acceleration = 10f;

    protected Vector3 _velocity = Vector3.zero;

    protected virtual void Update()
    {
        if (!_enabled) return;

        AcceleratedMovement();
        Anim(_velocity.magnitude / _moveSpeed);

        FixYPosToZero(); //TODO: Fix this
    }

    protected virtual void AcceleratedMovement()
    {
        Vector3 targetVelocity = _moveDirection;
        _velocity = Vector3.Lerp(_velocity, targetVelocity, _acceleration * Time.deltaTime);
        _characterController.Move(_velocity * Time.deltaTime);
    }

    protected virtual void FixYPosToZero()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    public override void KnockBack(float power)
    {
        _velocity *= -power;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        OnCollision(hit);
    }

    public virtual void OnCollision(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;
        // We dont want to push objects below us
        if (hit.moveDirection.y < 0f) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.AddForce(pushDir * _moveForce * _characterController.velocity.magnitude);
    }

    public override void Disable()
    {
        base.Disable();
        _characterController.enabled = false;
        _moveDirection = Vector3.zero;
    }

    public override void Enable()
    {
        base.Enable();
        _characterController.enabled = true;
        _moveDirection = Vector3.zero;
    }
}
