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

    public bool IsEquipped { get; private set; }
    public Rigidbody Rigidbody { get => _rigidbody; }
    public bool IsActive { get; private set; }

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
        //Data.Items.Remove(gameObject);
        gameObject.tag = "Untagged";
        IsEquipped = true;

        EnableRigidbody(false);
    }

    public virtual void Throw()
    {
        //Data.Items.Add(gameObject);
        gameObject.tag = "Item";
        IsEquipped = false;

        EnableRigidbody(true);
    }

    public virtual void Activate()
    {
        IsActive = true;
    }

    public virtual void Deactivate()
    {
        IsActive = false;
    }

    private void EnableRigidbody(bool value)
    {
        _rigidbody.isKinematic = !value;
        _boxCollider.enabled = value;
    }
}
