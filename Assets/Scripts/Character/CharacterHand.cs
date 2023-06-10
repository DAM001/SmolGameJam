using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHand : MonoBehaviour
{
    [SerializeField] private float _pickupDistance = 3f;

    private GameObject _currentItem;

    public GameObject CurrentItem { get => _currentItem; }

    private void Start()
    {
        if (transform.childCount == 0) return;
        _currentItem = transform.GetChild(0).gameObject;
        OnEquip();
    }

    public bool IsWeapon()
    {
        return _currentItem.GetComponent<WeaponScript>() != null;
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
        GetWeapon().FireDown();
    }

    public void OnFireUp()
    {
        if (_currentItem == null) return;
        GetWeapon().FireUp();
    }

    public void OnEquip()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        GameObject item = GameObjectUtil.FindClosest(items, transform.position);
        if (Vector3.Distance(item.transform.position, transform.position) > _pickupDistance) return;

        _currentItem = item;
        GetInventoryItem().Equip();
        if (IsWeapon()) GetWeapon().SetParent(gameObject);
    }

    public void OnThrow()
    {
        if (_currentItem == null) return;
        GetInventoryItem().Throw();
        if (IsWeapon()) GetWeapon().FireUp();

        _currentItem = null;
    }
}
