using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProgressType { Nope, Controlled, Automatic }

public class InventoryItem : MonoBehaviour
{
    [SerializeField] protected BoxCollider _boxCollider;
    [SerializeField] protected Rigidbody _rigidbody;
    [Header("Properties:")]
    [SerializeField] protected InventoryItemType _itemType;
    [SerializeField] protected int _stackSize = 1;
    [Space(5)]
    [SerializeField] protected float _throwForce = 1000f;
    [Header("UI:")]
    [SerializeField] protected ProgressType _progressType = ProgressType.Nope;
    [Space(10)]
    [SerializeField] protected GameObject _inventoryIcon;
    [SerializeField] protected Sprite _iconSprite;
    [SerializeField] protected Color32 _iconBackgroundColor;
    [SerializeField] protected float _iconScale = .03f;

    public InventoryItemType ItemType { get => _itemType; }
    public int StackSize { get => _stackSize; }
    public float ThrowForce { get => _throwForce; }
    public ProgressType ProgressType { get => _progressType; }

    public GameObject InventoryIcon { get => _inventoryIcon; }

    public bool IsEquipped { get; protected set; }
    public Rigidbody Rigidbody { get => _rigidbody; }
    public bool IsActive { get; protected set; }

    protected virtual void Start()
    {
        transform.parent = null;
        gameObject.tag = "Item";
        Data.Items.Add(gameObject);

        UpdateUiIcon();
    }

    protected void UpdateUiIcon()
    {
        GameObject icon = Instantiate(_inventoryIcon);
        _inventoryIcon = icon;

        UiInventoryItem itemIcon = icon.GetComponent<UiInventoryItem>();
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

    public virtual float Progress()
    {
        return 0f;
    }

    public virtual float Cooldown()
    {
        return 0f;
    }
}
