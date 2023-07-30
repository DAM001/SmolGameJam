using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHorse : VehicleBase
{
    public override void Enter(GameObject passanger)
    {
        base.Enter(passanger);

        _animator.Play("Move");
    }

    public override void Exit()
    {
        base.Exit();

        _animator.Play("Idle");
    }
}
