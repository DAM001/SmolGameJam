using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBase : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [Header("Properties:")]
    [SerializeField] protected float _moveSpeed = 13f;
    [SerializeField] protected float _rotationSpeed = 300f;
    [SerializeField] protected float _minDistance = .5f;
    [Space(10)]
    [SerializeField] protected float _moveForce = 2f;

    protected Vector3 _moveDirection = Vector3.zero;
    protected bool _enabled = true;

    public Animator Animator { get => _animator; }

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

    protected virtual void Anim(float speed)
    {
        if (_animator == null || !_enabled) return;
        _animator.speed = speed;
    }

    public virtual void KnockBack(float power)
    {
        //knockback code goes here
    }

    public void Disable()
    {
        _animator.speed = 0f;
        _enabled = false;
    }

    public void Enable()
    {
        _enabled = true;
    }
}
