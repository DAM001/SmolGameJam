using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiInventory : MonoBehaviour
{
    [SerializeField] private Transform _inventory;

    private readonly int _maxItems = 6;

    public void SetInventorySize(int size)
    {
        for (int i = _inventory.childCount - 1; i >= size; i--)
        {
            _inventory.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void UpdateItem(int index, GameObject item)
    {
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

    public GameObject GetItem(int index)
    {
        if (_inventory.GetChild(index).GetChild(1).childCount == 0) return null;
        return _inventory.GetChild(index).GetChild(1).GetChild(0).gameObject;
    }
}
