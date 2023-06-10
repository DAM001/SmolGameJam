using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHand : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
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
        if (IsWeapon()) GetWeapon().FireDown();
    }

    public void OnFireUp()
    {
        if (_currentItem == null) return;
        if (IsWeapon()) GetWeapon().FireUp();
    }

    public void OnEquip()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        GameObject item = GameObjectUtil.FindClosest(items, transform.position);
        if (Vector3.Distance(item.transform.position, transform.position) > _pickupDistance) return;

        _currentItem = item;
        GetInventoryItem().Equip();
        _inventory.AddItem(item);
        if (IsWeapon()) GetWeapon().SetParent(gameObject);
    }

    public void OnThrow()
    {
        if (_currentItem == null) return;
        GetInventoryItem().Throw();
        if (IsWeapon()) GetWeapon().FireUp();

        _inventory.ThrowItem();
        _currentItem = null;
    }

    public void ChangeInventoryIndex(int index)
    {
        _currentItem = _inventory.ChangeActive(index);
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
}
