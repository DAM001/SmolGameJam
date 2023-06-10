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
        _numberOfRoundsLeft = _magSize;
        _weaponScript.Parent.GetComponent<CharacterHand>().UseAmmo();
    }

    private IEnumerator ReloadHandler()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTime);
        _isReloading = false;
        Reload();
    }

    public float AmmoInPercentage()
    {
        return _numberOfRoundsLeft / _magSize;
    }
}
