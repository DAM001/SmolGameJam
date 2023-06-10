using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    [SerializeField] private WeaponType _weaponType;

    private readonly int _maxAmmo = 3;

    private int _currentAmmo = 1;

    public WeaponType WeaponType { get => _weaponType; }
    public int CurrentAmmo { get => _currentAmmo; }

    public int UseAmmo()
    {
        if (_currentAmmo == 0) return 0;

        _currentAmmo--;
        return _currentAmmo;
    }

    public bool AddAmmo()
    {
        if (_currentAmmo >= _maxAmmo) return false;

        _currentAmmo++;
        return true;
    }
}
