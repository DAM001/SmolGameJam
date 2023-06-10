using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public enum InventoryItemType { Weapon, Shield, Heal, Ammo, Grenade }

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _maxInventorySlots = 6;
    [SerializeField] private int _availableInventorySlots = 3;

    private readonly int _maxNumberOfWeapons = 2;

    private GameObject[] _items;
    private int _numberOfWeapons = 0;
    private int _activeIndex = 0;

    private void Start()
    {
        _items = new GameObject[_maxInventorySlots];
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiInventory>().SetInventorySize(_availableInventorySlots);
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

    public void UpgradeBackpack()
    {
        if (_availableInventorySlots >= _maxInventorySlots) return;
        _availableInventorySlots++;
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiInventory>().SetInventorySize(_availableInventorySlots);
    }

    public void AddItem(GameObject item)
    {
        if (item.GetComponent<InventoryItem>().ItemType == InventoryItemType.Weapon && _numberOfWeapons >= _maxNumberOfWeapons) return;

        if (_items[_activeIndex] == null)
        {
            _items[_activeIndex] = item;
            if (item.GetComponent<InventoryItem>().ItemType == InventoryItemType.Weapon) _numberOfWeapons++;
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiInventory>().UpdateItem(_activeIndex, item.GetComponent<InventoryItem>().InventoryIcon);
            return;
        }
    }

    public void ThrowItem()
    {
        if (_items[_activeIndex] == null) return;
        if (_items[_activeIndex].GetComponent<InventoryItem>().ItemType == InventoryItemType.Weapon) _numberOfWeapons--;
        _items[_activeIndex] = null;
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiInventory>().ClearItem(_activeIndex);
    }

    public void UseAmmo(WeaponType weaponType)
    {
        for (int i = 0; i < _availableInventorySlots; i++)
        {
            if (_items[i] != null && _items[i].GetComponent<InventoryItem>().ItemType == InventoryItemType.Ammo)
            {
                if (_items[i].GetComponent<AmmoBox>().WeaponType == weaponType)
                {
                    Debug.Log("Do the ammo stuffs here");
                }
            }
        }
    }

    public bool HasAmmo(WeaponType weaponType)
    {
        for (int i = 0; i < _availableInventorySlots; i++)
        {
            if (_items[i] != null && _items[i].GetComponent<InventoryItem>().ItemType == InventoryItemType.Ammo)
            {
                if (_items[i].GetComponent<AmmoBox>().WeaponType == weaponType)
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

        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiInventory>().SelectItem(index);
        return _items[index];
    }
}
