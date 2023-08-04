using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMag : MonoBehaviour
{
    [Header("Scripts:")]
    [SerializeField] private GunScript _weaponScript;
    [Header("Properties:")]
    [SerializeField] private int _magSize = 30;
    [SerializeField] private float _reloadTime = 2f;
    [SerializeField] private bool _autoReload = false;

    private int _numberOfRoundsLeft = 0;
    private bool _isReloading = false;
    private float _reloadTimeSlider = 0f;

    public bool IsReloading { get => _isReloading; }
    public float ReloadTime { get => _reloadTime; }

    private void Start()
    {
        _numberOfRoundsLeft = _magSize;
    }

    public void Activate()
    {
        _isReloading = false;
        if (_autoReload && _numberOfRoundsLeft == 0) ReloadStart();
    }

    public void Deactivate()
    {
        _isReloading = false;
        StopCoroutine("ReloadHandler");
    }

    public bool OnFire()
    {
        if (_numberOfRoundsLeft > 0)
        {
            _numberOfRoundsLeft--;
            if (_autoReload && _numberOfRoundsLeft == 0) ReloadStart();
            return true;
        }
 
        return false;
    }
    
    public void ReloadStart()
    {
        if (_isReloading) return;
        _numberOfRoundsLeft = 0;
        StartCoroutine("ReloadHandler");
    }

    private void ReloadEnd()
    {
        if (!_isReloading) return;
        _numberOfRoundsLeft = _magSize;
        _isReloading = false;

        _weaponScript.ReloadEnd();
    }

    private IEnumerator ReloadHandler()
    {
        float updateSpeed = .02f;
        _isReloading = true;
        _reloadTimeSlider = 0f;
        while (_reloadTimeSlider < _reloadTime)
        {
            yield return new WaitForSeconds(updateSpeed);
            _reloadTimeSlider += updateSpeed;
        }
        ReloadEnd();
    }

    public float AmmoInPercentage()
    {
        float ammo = (float)_numberOfRoundsLeft / (float)_magSize * 100f;
        float reloadTime = _reloadTimeSlider / _reloadTime * 100f;
        return _isReloading ? reloadTime : ammo;
    }
}
