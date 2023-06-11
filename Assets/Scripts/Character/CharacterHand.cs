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

    private void FixedUpdate()
    {
        if (!IsWeapon()) return;
        _inventory.UpdateWEaponUi(_currentItem.GetComponent<WeaponMag>().AmmoInPercentage());
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
        if (IsGrenade())
        {
            GameObject grenade = _currentItem;
            OnThrow();
            grenade.GetComponent<Grenade>().Throw();
        }
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
        ChangeInventoryIndex(_inventory.ActiveIndex);
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
        if (IsWeapon())
        {
            if (_currentItem.GetComponent<WeaponMag>().IsReloading) _inventory.UpdateWEaponUi(0f);
        }

        _currentItem = _inventory.ChangeActive(index);
        if (IsWeapon()) GetWeapon().Activate();
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
