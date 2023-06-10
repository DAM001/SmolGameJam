using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHand : MonoBehaviour
{
    private GameObject _currentWeapon;

    public GameObject Weapon { get => _currentWeapon; }

    private void Start()
    {
        if (transform.childCount == 0) return;
        OnEquip();
    }

    public WeaponScript GetWeapon()
    {
        if (_currentWeapon == null) return null;
        return _currentWeapon.GetComponent<WeaponScript>();
    }

    public InventoryItem GetInventoryItem()
    {
        if (_currentWeapon == null) return null;
        return _currentWeapon.GetComponent<InventoryItem>();
    }

    public void OnFireDown()
    {
        if (_currentWeapon == null) return;
        GetWeapon().FireDown();
    }

    public void OnFireUp()
    {
        if (_currentWeapon == null) return;
        GetWeapon().FireUp();
    }

    public void OnEquip()
    {
        _currentWeapon = transform.GetChild(0).gameObject;
        GetInventoryItem().Equip();
        GetWeapon().SetParent(gameObject);
    }

    public void OnThrow()
    {
        if (_currentWeapon == null) return;
        GetInventoryItem().Throw();
    }
}
