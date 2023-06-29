using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBase : MovementBase
{
    [SerializeField] private Vector3 _passangerPos = Vector3.zero;

    private GameObject _passanger;

    public bool InUse { get; set; }

    private void Start()
    {
        InUse = false;
    }

    private void LateUpdate()
    {
        if (_passanger == null) return;

        _passanger.transform.position = transform.position + _passangerPos;
        _passanger.transform.rotation = transform.rotation;
    }

    public virtual void Use(GameObject passanger)
    {
        InUse= true;

        _passanger = passanger;
    }

    public virtual void NotUse()
    {
        InUse = false;

        _passanger = null;
        _moveDirection = Vector3.zero;
    }

    public override void LookAt(Vector3 pos)
    {
        base.LookAt(transform.position + _moveDirection);
    }
}
