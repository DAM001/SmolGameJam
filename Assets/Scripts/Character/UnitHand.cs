using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHand : MonoBehaviour
{
    [SerializeField] private UnitHandItemMovement _itemMovment;
    [Header("Properties:")]
    [SerializeField] private float _pickupDistance = 3f;

    private int _currentItemIndex = 0;

    public GameObject CurrentItem { get; private set; }

    public void UseDown()
    {
        if (!HasItem()) return;

        CurrentItem.GetComponent<InventoryItem>().UseDown();
    }

    public void UseUp()
    {
        CurrentItem.GetComponent<InventoryItem>().UseUp();
    }

    public void Reload()
    {
        
    }

    public void Equip()
    {
        GameObject item = EquipableItem();
        if (item == null) return;

        CurrentItem = item;
        _itemMovment.EquipItem(item);
        CurrentItem.GetComponent<InventoryItem>().Equip();
    }

    public void ThrowActiveItem()
    {
        if (!HasItem()) return;

        CurrentItem.GetComponent<InventoryItem>().Throw();
        _itemMovment.ThrowItem(CurrentItem);
        CurrentItem = null;
    }

    public void ChangeInventoryIndex(int index)
    {
        int maxValue = 6; //Get this from the inventory
        if (_currentItemIndex < 0) _currentItemIndex = 0;
        else if (_currentItemIndex > maxValue) _currentItemIndex = maxValue;

        _currentItemIndex = index;
    }

    public void Scroll(int value)
    {
        _currentItemIndex += value;

        ChangeInventoryIndex(_currentItemIndex);
    }

    public bool HasItem()
    {
        return CurrentItem != null;
    }

    public GameObject EquipableItem() //TODO: use the Data.Items instead of FindGameObjects
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        GameObject item = GameObjectUtil.FindClosest(items, transform.position);
        if (Vector3.Distance(item.transform.position, transform.position) > _pickupDistance) return null;
        return item;
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
