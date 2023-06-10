using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Rigidbody _rigidbody;
    [Header("Properties:")]
    [SerializeField] private InventoryItemType _itemType;
    [SerializeField] private float _throwForce = 1000f;

    public InventoryItemType ItemType { get => _itemType; }

    public void Equip()
    {
        _rigidbody.isKinematic = true;
        _boxCollider.enabled = false;
        transform.parent = null;
    }

    public void Throw()
    {
        _rigidbody.isKinematic = false;
        _boxCollider.enabled = true;
        _rigidbody.AddForce(transform.forward * _throwForce + transform.up * _throwForce / 5f);
        _rigidbody.AddTorque(transform.up * Random.Range(-_throwForce / 10f, _throwForce / 10f));
    }
}
