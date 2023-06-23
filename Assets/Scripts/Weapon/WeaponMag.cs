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
        if (!_isReloading || !_weaponScript.Equipped) return;
        _numberOfRoundsLeft = 0;
        StartCoroutine(ReloadHandler());
    }

    private void Reload()
    {
        if (!_isReloading || !_weaponScript.Equipped) return;
        _numberOfRoundsLeft = _magSize;
        _isReloading = false;
    }

    private IEnumerator ReloadHandler()
    {
        float updateSpeed = .1f;
        _isReloading = true;
        _reloadTimeSlider = 0f;
        while (_reloadTimeSlider < _reloadTime)
        {
            yield return new WaitForSeconds(updateSpeed);
            _reloadTimeSlider += updateSpeed;
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
