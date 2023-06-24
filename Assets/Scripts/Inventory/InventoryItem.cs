using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Rigidbody _rigidbody;
    [Header("Properties:")]
    [SerializeField] private InventoryItemType _itemType;
    [SerializeField] private int _stackSize = 1;
    [Header("UI:")]
    [SerializeField] private GameObject _inventoryIcon;

    public InventoryItemType ItemType { get => _itemType; }
    public int StackSize { get => _stackSize; }
    public GameObject InventoryIcon { get => _inventoryIcon; }

    public bool Equipped { get; private set; }
    public Rigidbody Rigidbody { get => _rigidbody; }

    private void Start()
    {
        transform.parent = null;
    }

    public virtual void UseDown()
    {
        //use down logic goes here
    }

    public virtual void UseUp()
    {
        //use up logic goes here
    }

    public virtual void Equip(UnitHand unitHand)
    {
        Equip();
    }

    public virtual void Equip()
    {
        gameObject.tag = "Untagged";
        Equipped = true;

        EnableRigidbody(false);
    }

    public virtual void Throw()
    {
        gameObject.tag = "Item";
        Equipped = false;

        EnableRigidbody(true);
    }

    private void EnableRigidbody(bool value)
    {
        _rigidbody.isKinematic = !value;
        _boxCollider.enabled = value;
    }
}
