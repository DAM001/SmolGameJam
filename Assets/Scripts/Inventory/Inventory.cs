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

    private InventoryItem[] _items;
    private int _numberOfWeapons = 0;

    private void Start()
    {
        _items = new InventoryItem[_maxInventorySlots];
    }

    public void UpgradeBackpack()
    {
        if (_availableInventorySlots >= _maxInventorySlots) return;
        _availableInventorySlots++;
    }

    public void AddItem(InventoryItem item)
    {
        if (item.ItemType == InventoryItemType.Weapon && _numberOfWeapons >= _maxNumberOfWeapons) return;
        
        for (int i = 0; i < _availableInventorySlots; i++)
        {
            if (_items[i] == null)
            {
                _items[i] = item;
                if (item.ItemType == InventoryItemType.Weapon) _numberOfWeapons++;
            }
        }
    }

    public void ThrowItem(int index)
    {
        if (_items[index].ItemType == InventoryItemType.Weapon) _numberOfWeapons--;
        _items[index] = null;
    }
}
