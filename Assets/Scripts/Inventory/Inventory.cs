using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryItemType { Weapon, Shield, Health, Ammo, Grenade, BackpackUpgrade }

public class Inventory : MonoBehaviour
{
    [SerializeField] private UnitManager _manager;
    [Space(10)]
    [SerializeField] private int _maxInventorySlots = 6;
    [SerializeField] private int _availableInventorySlots = 3;

    private GameObject[] _items;
    private int _activeIndex = 0;

    public int ActiveIndex { get => _activeIndex; }
    public int MaxInventory { get => _availableInventorySlots; }

    private void Start()
    {
        _items = new GameObject[_maxInventorySlots];
        if (_manager.IsPlayer) GetUi().SetInventorySize(_availableInventorySlots);
    }

    public GameObject ChangeActive(int index)
    {
        _activeIndex = index;
        if (_activeIndex >= _availableInventorySlots)
        {
            _activeIndex = _availableInventorySlots - 1;
            return null;
        }
        return ChangeItem(index);
    }

    public void UpgradeBackpack(GameObject item)
    {
        if (_availableInventorySlots >= _maxInventorySlots) return;
        _availableInventorySlots++;
        Destroy(item);
        if (_manager.IsPlayer) GetUi().SetInventorySize(_availableInventorySlots);
    }

    public void AddItem(GameObject item)
    {
        switch(item.GetComponent<InventoryItem>().ItemType)
        {
            case InventoryItemType.BackpackUpgrade:
                UpgradeBackpack(item);
                return;
            case InventoryItemType.Ammo:
                if (AddAmmoItem(item)) return;
                break;
        }

        //active index
        if (_items[_activeIndex] == null)
        {
            AddItemLogic(_activeIndex, item);
            return;
        }

        //search for empty slot
        for (int i = 0; i < _availableInventorySlots; i++)
        {
            if (_items[i] == null)
            {
                AddItemLogic(i, item);
                return;
            }
        }
    }

    private void AddItemLogic(int i, GameObject item)
    {
        _items[i] = item;
        if (_manager.IsPlayer) GetUi().UpdateItem(i, item.GetComponent<InventoryItem>().InventoryIcon);

        if (item.GetComponent<InventoryItem>().ItemType == InventoryItemType.Ammo)
        {
            if (_manager.IsPlayer) GetUi().GetItem(i).GetComponent<UiAmmo>().UpdateValue(item.GetComponent<AmmoItem>().CurrentAmmo);
        }
    }

    private bool AddAmmoItem(GameObject item)
    {
        WeaponType weaponType = item.GetComponent<AmmoItem>().WeaponType;
        if (HasAmmo(weaponType))
        {
            int index = GetNotFullAmmoIndex(weaponType);
            if (index == -1) return false;

            Destroy(item);
            _items[index].GetComponent<AmmoItem>().AddAmmo();
            if (_manager.IsPlayer) GetUi().GetItem(index).GetComponent<UiAmmo>().UpdateValue(_items[index].GetComponent<AmmoItem>().CurrentAmmo);
            return true;
        }
        return false;
    }

    public void ThrowItem()
    {
        ThrowItemWithIndex(_activeIndex);
    }

    private void ThrowItemWithIndex(int index)
    {
        if (_items[index] == null) return;
        _items[index] = null;
        if (_manager.IsPlayer) GetUi().ClearItem(index);
    }

    public void UseAmmo(WeaponType weaponType)
    {
        int ammoIdx = GetAmmoIndex(weaponType);
        if (ammoIdx == -1) return;

        int ammoLeft = _items[ammoIdx].GetComponent<AmmoItem>().UseAmmo();
        if (_manager.IsPlayer) GetUi().GetItem(GetAmmoIndex(weaponType)).GetComponent<UiAmmo>().UpdateValue(_items[ammoIdx].GetComponent<AmmoItem>().CurrentAmmo);

        if (ammoLeft != 0) return;
        GameObject emptyAmmo = _items[ammoIdx];
        ThrowItemWithIndex(ammoIdx);
        Destroy(emptyAmmo);
    }

    private int GetAmmoIndex(WeaponType weaponType)
    {
        for (int i = 0; i < _availableInventorySlots; i++)
        {
            if (_items[i] != null && _items[i].GetComponent<InventoryItem>().ItemType == InventoryItemType.Ammo)
            {
                if (_items[i].GetComponent<AmmoItem>().WeaponType == weaponType)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    private int GetNotFullAmmoIndex(WeaponType weaponType)
    {
        for (int i = 0; i < _availableInventorySlots; i++)
        {
            if (_items[i] != null && _items[i].GetComponent<InventoryItem>().ItemType == InventoryItemType.Ammo)
            {
                if (_items[i].GetComponent<AmmoItem>().WeaponType == weaponType)
                {
                    if (_items[i].GetComponent<AmmoItem>().CurrentAmmo < 3)
                    {
                        return i;
                    }
                }
            }
        }
        return -1;
    }

    public bool HasAmmo(WeaponType weaponType)
    {
        for (int i = 0; i < _availableInventorySlots; i++)
        {
            if (_items[i] != null && _items[i].GetComponent<InventoryItem>().ItemType == InventoryItemType.Ammo)
            {
                if (_items[i].GetComponent<AmmoItem>().WeaponType == weaponType)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public GameObject ChangeItem(int index)
    {
        if (index >= _availableInventorySlots) return null;
        _activeIndex = index;

        for (int i = 0; i < _availableInventorySlots; i++)
        {
            if (_items[i] != null)
            {
                if (i == index) _items[i].SetActive(true);
                else _items[i].SetActive(false);
            }
        }

        if (_manager.IsPlayer) GetUi().SelectItem(index);
        return _items[index];
    }

    private UiInventory GetUi()
    {
        return GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiInventory>();
    }

    public void UpdateProgress(float value)
    {
        if (_manager.IsPlayer) GetUi().GetItem(_activeIndex).GetComponent<UiProgress>().UpdateProgress(value);
    }

    public bool HasEmptySlot()
    {
        for (int i = 0; i < _availableInventorySlots; i++)
        {
            if (_items[i] == null) return true;
        }

        return false;
    }
}
