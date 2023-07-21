using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : InventoryItem
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _throwForce = 5000f;

    public override void UseDown()
    {
        Throw();

        _rigidbody.AddForce(transform.forward * _throwForce);
    }

    protected override void EnableRigidbody(bool value)
    {
        _rigidbody.isKinematic = !value;
        _sphereCollider.enabled = value;
    }
}
