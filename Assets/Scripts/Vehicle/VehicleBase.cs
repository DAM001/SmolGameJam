using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBase : MovementBase
{
    [Header("Vehicle:")]
    [SerializeField] private Vector3 _passangerPos = Vector3.zero;
    [SerializeField] private GameObject _visualsFolder;

    private GameObject _passanger;

    public bool InUse { get; set; }

    protected virtual void Start()
    {
        transform.parent = null;

        Exit();
    }

    protected virtual void LateUpdate()
    {
        if (_passanger == null) return;

        _passanger.transform.position = _visualsFolder.transform.position + _visualsFolder.transform.TransformDirection(_passangerPos);
        _passanger.transform.rotation = _visualsFolder.transform.rotation;
    }

    public virtual void Enter(GameObject passanger)
    {
        InUse = true;

        _passanger = passanger;
    }

    public virtual void Exit()
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
