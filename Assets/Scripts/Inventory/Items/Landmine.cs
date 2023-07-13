using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : InventoryItem
{
    [Header("Landmine:")]
    [SerializeField] private float _activationMass = 50f;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private Explotion _explotion;


    public override void UseDown()
    {
        EnableRigidbody(true);
        IsEquipped = false;

        _sphereCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null || rb.mass <= _activationMass) return;

        _explotion.Explode();
        Data.Items.Remove(gameObject);
        Destroy(gameObject);
    }

    public override void Equip()
    {
        base.Equip();

        _sphereCollider.enabled = false;
    }

    public override void Throw()
    {
        base.Throw();

        _sphereCollider.enabled = true;
    }
}
