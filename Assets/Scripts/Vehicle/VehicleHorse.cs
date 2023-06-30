using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHorse : VehicleBase
{
    public override void Use(GameObject passanger)
    {
        base.Use(passanger);

        _animator.Play("Move");
    }

    public override void NotUse()
    {
        base.NotUse();

        _animator.Play("Idle");
    }
}
