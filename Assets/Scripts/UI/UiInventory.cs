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
            UpdateIcon(i);
        }
    }

    private void UpdateIcon(int index)
    {
        Inventory inventory = _player.GetComponent<Inventory>();
        GameObject itemIcon = inventory.ItemIcon(index);
        if (itemIcon == null) ClearItem(index);
        else
        {
            UpdateItem(index, itemIcon);

            GameObject item = _player.GetComponent<Inventory>().GetInventoryItem(index);
            ProgressType progressType = item.GetComponent<InventoryItem>().ProgressType;
            int stackSize = item.GetComponent<InventoryItem>().StackSize;

            if (progressType != ProgressType.Nope)
            {
                float value = item.GetComponent<InventoryItem>().Progress();
                if (progressType == ProgressType.Controlled)
                {
                    // nothing here
                }
                else if (progressType == ProgressType.Automatic)
                {
                    float cooldown = item.GetComponent<InventoryItem>().Cooldown();
                    value = (value - Time.time) / cooldown * 100f;
                    if (value < 0f) value = 0f;
                }

                UiProgress uiProgress = GetItemIcon(index).GetComponent<UiProgress>();
                uiProgress.Show(true);
                uiProgress.UpdateProgress(value);
            }
            else if (stackSize > 1)
            {
                int quantity = inventory.NumberOfItems(index);
                UiCounter uiCounter = GetItemIcon(index).GetComponent<UiCounter>();
                uiCounter.Show(true);
                uiCounter.UpdateText(quantity + "/" + stackSize);
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
