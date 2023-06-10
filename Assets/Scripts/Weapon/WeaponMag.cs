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
            return true;
        }
 
        OnReload();
        return false;
    }
    
    public void OnReload()
    {
        if (!_weaponScript.Parent.GetComponent<CharacterHand>().HasAmmo()) return;
        if (_isReloading) return;
        StartCoroutine(ReloadHandler());
    }

    private void Reload()
    {
        if (!_isReloading) return;
        _weaponScript.Parent.GetComponent<CharacterHand>().UseAmmo();
        _numberOfRoundsLeft = _magSize;
        _isReloading = false;
    }

    private IEnumerator ReloadHandler()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTime);
        Reload();
    }

    public float AmmoInPercentage()
    {
        return _numberOfRoundsLeft / _magSize;
    }
}
