using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHand : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private CharacterHealth _health;
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
        if (IsWeapon()) _inventory.UpdateProgress(_currentItem.GetComponent<WeaponMag>().AmmoInPercentage());
        if (IsHealthItem()) _inventory.UpdateProgress(_currentItem.GetComponent<HealthItem>().Progress());
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

    public bool IsHealthItem()
    {
        if (_currentItem == null) return false;
        return _currentItem.GetComponent<HealthItem>() != null;
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
        else if (IsHealthItem())
        {
            _currentItem.GetComponent<HealthItem>().OnUse();
        }
    }

    public void OnFireUp()
    {
        if (_currentItem == null) return;
        if (IsWeapon()) GetWeapon().FireUp();
        else if (IsGrenade())
        {
            UsedUpItem().GetComponent<Grenade>().Throw();
        }
    }

    public void Reload()
    {
        if (!IsWeapon()) return;
        GetWeapon().Reload();
    }

    public GameObject UsedUpItem()
    {
        GameObject item = _currentItem;
        OnThrow();
        return item;
    }

    public void OnEquip()
    {
        GameObject item = EquipableItem();
        if (item == null) return;

        _currentItem = item;
        GetInventoryItem().Equip();
        _inventory.AddItem(item);
        if (IsWeapon()) GetWeapon().SetParent(gameObject);
        ChangeInventoryIndex(_inventory.ActiveIndex);
    }

    public GameObject EquipableItem()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        GameObject item = GameObjectUtil.FindClosest(items, transform.position);
        if (Vector3.Distance(item.transform.position, transform.position) > _pickupDistance) return null;
        return item;
    }

    public void OnThrow()
    {
        if (_currentItem == null) return;
        GetInventoryItem().Throw();
        if (IsWeapon())
        {
            GetWeapon().FireUp();
            GetWeapon().SetParent(null);
        }

        _inventory.ThrowItem();
        _currentItem = null;
    }

    public void ChangeInventoryIndex(int index)
    {
        if (IsWeapon())
        {
            if (_currentItem.GetComponent<WeaponMag>().IsReloading) _inventory.UpdateProgress(0f);
        }
        else if (IsHealthItem())
        {
            if (_currentItem.GetComponent<HealthItem>().IsUsing) _inventory.UpdateProgress(0f);
        }

        _currentItem = _inventory.ChangeActive(index);
        if (IsWeapon()) GetWeapon().Activate();
        if (IsHealthItem()) _currentItem.GetComponent<HealthItem>().Activate(GetComponent<CharacterHand>(), _health);
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

    public void ThrowEverything()
    {
        for (int i = 0; i < _inventory.MaxInventory; i++)
        {
            ChangeInventoryIndex(i);
            OnThrow();
        }
    }
}
