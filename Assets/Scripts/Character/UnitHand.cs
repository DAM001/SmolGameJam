using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHand : MonoBehaviour
{
    [Header("Scripts:")]
    [SerializeField] private UnitHandItemMovement _itemMovement;
    [SerializeField] private Inventory _inventory;
    [Header("Properties:")]
    [SerializeField] private GameObject _handObject;
    [SerializeField] private float _pickupDistance = 3f;

    public GameObject CurrentItem { get; private set; }
    public GameObject HandObject { get => _handObject; }

    public void UseDown()
    {
        if (!HasItem()) return;

        CurrentItem.GetComponent<InventoryItem>().UseDown();
    }

    public void UseUp()
    {
        if (!HasItem()) return;

        CurrentItem.GetComponent<InventoryItem>().UseUp();
    }

    public void Reload()
    {
        if (!HasItem()) return;

        if (ItemType(CurrentItem) == InventoryItemType.Weapon)
        {
            CurrentItem.GetComponent<WeaponScript>().Reload();
        }
    }

    public void Equip()
    {
        GameObject item = EquipableItem();
        if (item == null) return;

        if (!_inventory.EquipItem(item)) return;
        item.GetComponent<InventoryItem>().Equip(this);

        GameObject activeItem = _inventory.GetActiveItem();
        if (activeItem != CurrentItem)
        {
            CurrentItem = _inventory.GetActiveItem();
            _itemMovement.EquipItem(CurrentItem);
        }
    }

    public void ThrowActiveItem()
    {
        if (!HasItem()) return;

        if (!_inventory.ThrowActiveItem()) return;
        UseUp();
        CurrentItem.GetComponent<InventoryItem>().Throw();
        _itemMovement.ThrowItem(CurrentItem);
        CurrentItem = null;
    }

    public void ChangeInventoryIndex(int index)
    {
        UseUp();
        CurrentItem = _inventory.GetItem(index);
        if (!HasItem()) return;
        _itemMovement.SwitchItem(CurrentItem);
    }

    public void Scroll(int value)
    {
        _inventory.ActiveIndex += value;
        ChangeInventoryIndex(_inventory.ActiveIndex);
    }

    public bool HasItem()
    {
        return CurrentItem != null && CurrentItem.GetComponent<InventoryItem>() != null;
    }

    public InventoryItemType ItemType(GameObject item)
    {
        return item.GetComponent<InventoryItem>().ItemType;
    }

    public GameObject EquipableItem()
    {
        List<GameObject> notEquippedItems = new List<GameObject>();
        for (int i = 0; i < Data.Items.Count; i++)
        {
            if (!Data.Items[i].GetComponent<InventoryItem>().Equipped)
            {
                notEquippedItems.Add(Data.Items[i]);
            }
        }
        GameObject item = GameObjectUtil.FindClosest(notEquippedItems.ToArray(), _handObject.transform.position);
        if (Vector3.Distance(item.transform.position, _handObject.transform.position) > _pickupDistance) return null;
        return item;
    }

    public void KnockBack(float damage)
    {
        _itemMovement.KnockBack(damage);
    }
}

/*
public class UnitHand : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private UnitHealth _health;
    [Header("Properties:")]
    [SerializeField] private float _pickupDistance = 3f;

    private GameObject _currentItem;

    public GameObject CurrentItem { get => _currentItem; }

    private void Start()
    {
        if (transform.childCount == 0) return;
        _currentItem = transform.GetChild(0).gameObject;
        OnEquip();
    }

    private void FixedUpdate()
    {
        if (IsWeapon()) _inventory.UpdateProgress(_currentItem.GetComponent<WeaponMag>().AmmoInPercentage());
        if (IsHealthItem()) _inventory.UpdateProgress(_currentItem.GetComponent<HealthItem>().Progress());
    }

    public bool IsWeapon()
    {
        if (_currentItem == null) return false;
        return _currentItem.GetComponent<WeaponScript>() != null;
    }

    public bool IsGrenade()
    {
        if (_currentItem == null) return false;
        return _currentItem.GetComponent<Grenade>() != null;
    }

    public bool IsHealthItem()
    {
        if (_currentItem == null) return false;
        return _currentItem.GetComponent<HealthItem>() != null;
    }

    public WeaponScript GetWeapon()
    {
        if (_currentItem == null) return null;
        return _currentItem.GetComponent<WeaponScript>();
    }

    public InventoryItem GetInventoryItem()
    {
        if (_currentItem == null) return null;
        return _currentItem.GetComponent<InventoryItem>();
    }

    public void OnFireDown()
    {
        if (_currentItem == null) return;
        if (IsWeapon()) GetWeapon().FireDown();
        else if (IsHealthItem())
        {
            _currentItem.GetComponent<HealthItem>().OnUse();
        }
    }

    public void OnFireUp()
    {
        if (_currentItem == null) return;
        if (IsWeapon()) GetWeapon().FireUp();
        else if (IsGrenade())
        {
            UsedUpItem().GetComponent<Grenade>().Throw();
        }
    }

    public void Reload()
    {
        if (!IsWeapon()) return;
        GetWeapon().Reload();
    }

    public GameObject UsedUpItem()
    {
        GameObject item = _currentItem;
        OnThrow();
        return item;
    }

    public void OnEquip()
    {
        GameObject item = EquipableItem();
        InventoryItemType itemType = item.GetComponent<InventoryItem>().ItemType;
        if (item == null || (!_inventory.HasEmptySlot() && itemType != InventoryItemType.Ammo && itemType != InventoryItemType.BackpackUpgrade)) return;

        _currentItem = item;
        GetInventoryItem().Equip();
        _inventory.AddItem(item);
        if (IsWeapon()) GetWeapon().SetParent(gameObject);
        ChangeInventoryIndex(_inventory.ActiveIndex);
    }

    public GameObject EquipableItem()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        GameObject item = GameObjectUtil.FindClosest(items, transform.position);
        if (Vector3.Distance(item.transform.position, transform.position) > _pickupDistance) return null;
        return item;
    }

    public void OnThrow()
    {
        if (_currentItem == null) return;
        GetInventoryItem().Throw();
        if (IsWeapon())
        {
            GetWeapon().FireUp();
            GetWeapon().SetParent(null);
        }

        _inventory.ThrowItem();
        _currentItem = null;
    }

    public void ChangeInventoryIndex(int index)
    {
        if (IsWeapon())
        {
            if (_currentItem.GetComponent<WeaponMag>().IsReloading) _inventory.UpdateProgress(0f);
        }
        else if (IsHealthItem())
        {
            if (_currentItem.GetComponent<HealthItem>().IsUsing) _inventory.UpdateProgress(0f);
        }

        _currentItem = _inventory.ChangeActive(index);
        if (IsWeapon()) GetWeapon().Activate();
        if (IsHealthItem()) _currentItem.GetComponent<HealthItem>().Activate(GetComponent<UnitHand>(), _health);
    }

    public void UseAmmo()
    {
        if (!IsWeapon()) return;
        _inventory.UseAmmo(GetWeapon().WeaponType);
    }

    public bool HasAmmo()
    {
        if (!IsWeapon()) return false;
        return _inventory.HasAmmo(GetWeapon().WeaponType);
    }

    public void ThrowEverything()
    {
        for (int i = 0; i < _inventory.MaxInventory; i++)
        {
            ChangeInventoryIndex(i);
            OnThrow();
        }
    }
}
*/
