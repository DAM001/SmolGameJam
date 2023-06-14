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
    [Header("UI:")]
    [SerializeField] private GameObject _inventoryIcon;

    public InventoryItemType ItemType { get => _itemType; }
    public GameObject InventoryIcon { get => _inventoryIcon; }

    public bool Equipped { get; set; }

    private void Start()
    {
        transform.parent = null;
    }

    public void Equip()
    {
        gameObject.tag = "Untagged";
        Equipped = true;

        _rigidbody.isKinematic = true;
        _boxCollider.enabled = false;
        transform.parent = null;
    }

    public void Throw()
    {
        gameObject.tag = "Item";
        Equipped = false;

        _rigidbody.isKinematic = false;
        _boxCollider.enabled = true;
        _rigidbody.AddForce(transform.forward * _throwForce + transform.up * _throwForce / 5f);
        _rigidbody.AddTorque(transform.up * Random.Range(-_throwForce / 10f, _throwForce / 10f));
    }
}
