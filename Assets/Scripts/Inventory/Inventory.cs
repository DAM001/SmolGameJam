using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StackableItems
{
    private List<GameObject> _items;

    public int MaxSize { get; private set; }
    public int CurrentSize { get => _items.Count; }
    public GameObject Item
    { 
        get
        {
            if (_items.Count == 0) return null;
            return _items[0];
        }
        set
        {
            if (value == null)
            {
                _items.Remove(_items[0]);
                return;
            }

            _items.Add(value);
            MaxSize = value.GetComponent<InventoryItem>().StackSize;
            value.SetActive(false);
        }
    }

    public StackableItems()
    {
        _items = new List<GameObject>();
        MaxSize = 0;
    }
}

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _maxAvailableSlots = 6;
    [SerializeField] private int _currentlyAvailableSlots = 3;

    private StackableItems[] _items;
    private int _activeIndex = 0;

    public int ActiveIndex
    {
        get => _activeIndex;
        set {
            _activeIndex = value;
            if (_activeIndex < 0) _activeIndex = 0;
            else if (_activeIndex > _currentlyAvailableSlots - 1) _activeIndex = _currentlyAvailableSlots - 1;
        }
    }
    public int AvailableSlots { get => _currentlyAvailableSlots; }

    private void Start()
    {
        _items = new StackableItems[_maxAvailableSlots];
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i] = new StackableItems();
        }
    }

    public GameObject ItemIcon(int index)
    {
        if (_items[index].Item == null) return null;
        return _items[index].Item.GetComponent<InventoryItem>().InventoryIcon;
    }

    public GameObject GetActiveItem()
    {
        return GetItem(ActiveIndex);
    }

    public bool ThrowActiveItem()
    {
        if (_items[_activeIndex].Item == null) return false;

        ThrowItem(_activeIndex);
        return true;
    }

    public bool EquipItem(GameObject item)
    {
        if (AddToStack(item)) return true;
        if (!HasEmptySlot()) return false;

        AddItem(item, _activeIndex);
        return true;
    }

    public void AddItem(GameObject item, int index)
    {
        if (_items[index].Item == null)
        {
            _items[index].Item = item;
            return;
        }

        AddItemToEmptySlot(item);
    }

    private void AddItemToEmptySlot(GameObject item)
    {
        for (int i = 0; i < AvailableSlots; i++)
        {
            if (_items[i].Item == null)
            {
                _items[i].Item = item;
                item.SetActive(false);
                return;
            }
        }
    }

    public void ThrowItem(int index)
    {
        if (_items[index].Item == null) return;

        _items[index].Item.GetComponent<InventoryItem>().Deactivate();
        _items[index].Item = null;
    }

    public GameObject GetItem(int index)
    {
        ActiveIndex = index;

        for (int i = 0; i < AvailableSlots; i++)
        {
            if (_items[i].Item != null)
            {
                ActivateItemByIndex(i, i == index);
            }
        }

        return _items[index].Item;
    }

    public bool HasEmptySlot()
    {
        for (int i = 0; i < AvailableSlots; i++)
        {
            if (_items[i].Item == null) return true;
        }

        return false;
    }

    public GameObject GetInventoryItem(int index)
    {
        if (_items == null) return null;

        if (index < 0) index = 0;
        else if (index > AvailableSlots) index = AvailableSlots;
        return _items[index].Item;
    }

    private bool AddToStack(GameObject item)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].Item != null)
            {
                if (_items[i].MaxSize > _items[i].CurrentSize)
                {
                    if (_items[i].Item.GetComponent<InventoryItem>().ItemType == item.GetComponent<InventoryItem>().ItemType)
                    {
                        _items[i].Item = item;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public int GetGameObjectByType(InventoryItemType type)
    {
        for (int i = _items.Length - 1; i > 0; i--)
        {
            if (_items[i].Item != null)
            {
                if (_items[i].Item.GetComponent<InventoryItem>().ItemType == type)
                {
                    return i;
                }
            }
        }

        return -1;
    }

    public void ActivateItemByIndex(int index, bool active)
    {
        if (active) _items[index].Item.GetComponent<InventoryItem>().Activate();
        else _items[index].Item.GetComponent<InventoryItem>().Deactivate();
        _items[index].Item.SetActive(active);
    }

    public int NumberOfItems(int index)
    {
        return _items[index].CurrentSize;
    }
}