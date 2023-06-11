using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMag : MonoBehaviour
{
    [SerializeField] private WeaponScript _weaponScript;

    [SerializeField] private int _magSize = 30;
    [SerializeField] private float _reloadTime = 2f;

    private int _numberOfRoundsLeft = 0;
    private bool _isReloading = false;
    private float _reloadTimeSlider = 0f;

    public bool IsReloading { get => _isReloading; }

    private void Start()
    {
        _numberOfRoundsLeft = _magSize;
    }

    public void Activate()
    {
        _isReloading = false;
        if (_numberOfRoundsLeft == 0 && !_isReloading) OnReload();
    }

    public bool OnFire()
    {
        if (_numberOfRoundsLeft > 0)
        {
            _numberOfRoundsLeft--;
            if (_numberOfRoundsLeft == 0) OnReload();
            return true;
        }
 
        return false;
    }
    
    public void OnReload()
    {
        if (_weaponScript.Parent == null || !_weaponScript.Parent.GetComponent<CharacterHand>().HasAmmo()) return;
        if (_isReloading) return;
        StartCoroutine(ReloadHandler());
    }

    private void Reload()
    {
        if (!_isReloading || _weaponScript.Parent == null) return;
        _weaponScript.Parent.GetComponent<CharacterHand>().UseAmmo();
        _numberOfRoundsLeft = _magSize;
        _isReloading = false;
    }

    private IEnumerator ReloadHandler()
    {
        _isReloading = true;
        _reloadTimeSlider = 0f;
        while (_reloadTimeSlider < _reloadTime)
        {
            yield return new WaitForSeconds(.1f);
            _reloadTimeSlider += .1f;
        }
        Reload();
    }

    public float AmmoInPercentage()
    {
        float ammo = (float)_numberOfRoundsLeft / (float)_magSize * 100f;
        float reloadTime = _reloadTimeSlider / _reloadTime * 100f;
        return _isReloading ? reloadTime : ammo;
    }
}
