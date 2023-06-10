using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMag : MonoBehaviour
{
    [SerializeField] private int _magSize = 30;
    [SerializeField] private float _reloadTime = 2f;
    [SerializeField] private int _maxNumberOfMags = 4;
    [SerializeField] private int _numberOfMags = 3;

    private int _numberOfRoundsLeft = 0;
    private bool _isReloading = false;

    public bool IsReloading { get => _isReloading; }

    private void Start()
    {
        Reload();
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

    public void AddMag()
    {
        if (_maxNumberOfMags <= _numberOfMags) return;
        _numberOfMags++;
    }
    
    public void OnReload()
    {
        if (_numberOfMags <= 0) return;
        if (_isReloading) return;
        StartCoroutine(ReloadHandler());
    }

    private void Reload()
    {
        _numberOfRoundsLeft = _magSize;
        _numberOfMags--;
    }

    private IEnumerator ReloadHandler()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTime);
        _isReloading = false;
        Reload();
    }
}
