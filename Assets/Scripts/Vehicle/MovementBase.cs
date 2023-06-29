using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBase : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [Header("Properties:")]
    [SerializeField] private float _moveSpeed = 13f;
    [SerializeField] private float _rotationSpeed = 300f;
    [SerializeField] private float _minDistance = .5f;
    [SerializeField] private float _acceleration = 10f;
    [Space(10)]
    //[SerializeField] private float _gravity = 9.8f; //TODO: Add this to fix the y = 0
    [SerializeField] private float _moveForce = 1000f;

    protected Vector3 _moveDirection = Vector3.zero;
    protected Vector3 _velocity = Vector3.zero;

    protected virtual void Update()
    {
        AcceleratedMovement();
        Anim();

        FixYPosToZero(); //TODO: Fix this
    }

    public virtual void Move(Vector2 direction)
    {
        _moveDirection = new Vector3(direction.x, 0f, direction.y) * _moveSpeed;
    }

    public virtual void LookAt(Vector3 pos)
    {
        pos = new Vector3(pos.x, 0f, pos.z);
        if (Vector3.Distance(transform.position, pos) < _minDistance) return;
        Quaternion targetRot = Quaternion.LookRotation(pos - transform.position, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, _rotationSpeed * Time.deltaTime);
    }

    public virtual void LookAtController(Vector2 lookDir)
    {
        Vector3 pos = new Vector3(lookDir.x, 0f, lookDir.y);
        pos += transform.position;
        LookAt(pos);
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

    protected virtual void Anim()
    {
        if (_animator == null) return;
        _animator.speed = _characterController.velocity.magnitude / _moveSpeed;
    }

    public virtual void KnockBack(float power)
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
}
