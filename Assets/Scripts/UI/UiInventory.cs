using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiInventory : MonoBehaviour
{
    [SerializeField] private Transform _inventory;
    [SerializeField] private int _inventorySize = 6;

    private GameObject _player;

    private void Update()
    {
        if (_player == null) 
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        SelectItem(_player.GetComponent<Inventory>().ActiveIndex);
        int inventorySize = _player.GetComponent<Inventory>().AvailableSlots;
        SetInventorySize(inventorySize);
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject itemIcon = _player.GetComponent<Inventory>().ItemIcon(i);
            if (itemIcon == null) ClearItem(i);
            else
            {
                UpdateItem(i, itemIcon);

                GameObject item = _player.GetComponent<Inventory>().GetInventoryItem(i);
                InventoryItemType itemType = item.GetComponent<InventoryItem>().ItemType;
                if (itemType == InventoryItemType.Weapon)
                {
                    float value = item.GetComponent<WeaponScript>().AmmoInPercentage();
                    GetItemIcon(i).GetComponent<UiProgress>().UpdateProgress(value);
                }
            }
        }
    }

    public void SetInventorySize(int size)
    {
        for (int i = _inventorySize - 1; i >= 0; i--)
        {
            _inventory.GetChild(i).gameObject.SetActive(i < size);
        }
    }

    public void UpdateItem(int index, GameObject item)
    {
        if (GetInner(index).childCount > 0) return;
        GameObject itemImage = Instantiate(item, GetInner(index));
    }

    public void ClearItem(int index)
    {
        if (GetInner(index).childCount == 0) return;
        Destroy(GetInner(index).GetChild(0).gameObject);
    }

    public void SelectItem(int index)
    {
        for (int i = _inventory.childCount - 1; i >= 0; i--)
        {
            _inventory.GetChild(i).GetChild(0).gameObject.SetActive(i == index);
        }
    }

    private RectTransform GetInner(int index)
    {
        return _inventory.GetChild(index).GetChild(1).GetComponent<RectTransform>();
    }

    public GameObject GetItemIcon(int index)
    {
        if (_inventory.GetChild(index).GetChild(1).childCount == 0) return null;
        return _inventory.GetChild(index).GetChild(1).GetChild(0).gameObject;
    }
}
