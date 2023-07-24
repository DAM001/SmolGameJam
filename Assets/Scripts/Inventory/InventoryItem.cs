using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] protected BoxCollider _boxCollider;
    [SerializeField] protected Rigidbody _rigidbody;
    [Header("Properties:")]
    [SerializeField] protected InventoryItemType _itemType;
    [SerializeField] protected int _stackSize = 1;
    [Header("UI:")]
    [SerializeField] protected GameObject _inventoryIcon;
    [SerializeField] protected Sprite _iconSprite;
    [SerializeField] protected Color32 _iconBackgroundColor;
    [SerializeField] protected float _iconScale = .03f;

    public InventoryItemType ItemType { get => _itemType; }
    public int StackSize { get => _stackSize; }
    public GameObject InventoryIcon { get => _inventoryIcon; }

    public bool IsEquipped { get; protected set; }
    public Rigidbody Rigidbody { get => _rigidbody; }
    public bool IsActive { get; protected set; }

    protected virtual void Start()
    {
        transform.parent = null;
        gameObject.tag = "Item";

        UpdateUiIcon();
    }

    protected void UpdateUiIcon()
    {
        UiInventoryItem itemIcon = _inventoryIcon.GetComponent<UiInventoryItem>();
        itemIcon.SetIconImage(_iconSprite);
        itemIcon.SetBackgroundColor(_iconBackgroundColor);
        itemIcon.SetIconScale(_iconScale);
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

    protected virtual void EnableRigidbody(bool value)
    {
        _rigidbody.isKinematic = !value;
        _boxCollider.enabled = value;
    }
}
