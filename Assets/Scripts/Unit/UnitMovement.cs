using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [Header("Properties:")]
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _minDistance = .5f;
    [SerializeField] private float _acceleration = .05f;
    [Space(10)]
    [SerializeField] private float _gravity = 9.8f; //TODO: Add this to fix the y = 0
    [SerializeField] private float _moveForce = 1000f;

    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;

    public void Move(Vector2 direction)
    {
        _moveDirection = new Vector3(direction.x, 0f, direction.y) * _moveSpeed;
    }

    public void LookAt(Vector3 pos)
    {
        pos = new Vector3(pos.x, 0f, pos.z);
        if (Vector3.Distance(transform.position, pos) < _minDistance) return;
        Quaternion targetRot = Quaternion.LookRotation(pos - transform.position, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, _rotationSpeed * Time.deltaTime);
    }

    public void LookAtController(Vector2 lookDir)
    {
        Vector3 pos = new Vector3(lookDir.x, 0f, lookDir.y);
        pos += transform.position;

        LookAt(pos);
    }

    private void Update()
    {
        AcceleratedMovement();
        FixYPosToZero(); //TODO: Fix this
        Anim();
    }

    private void AcceleratedMovement()
    {
        Vector3 targetVelocity = _moveDirection;
        _velocity = Vector3.Lerp(_velocity, targetVelocity, _acceleration * Time.deltaTime);
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void FixYPosToZero()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    private void Anim()
    {
        _animator.speed = _characterController.velocity.magnitude / _moveSpeed;
    }

    public void KnockBack(float power)
    {
        _velocity *= -power;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < 0f) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.AddForce(pushDir * _moveForce * _characterController.velocity.magnitude);
    }
}
