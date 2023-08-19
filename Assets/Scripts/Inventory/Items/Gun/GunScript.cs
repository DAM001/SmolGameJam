using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Light, Heavy, Shotgun, Sniper }

public class GunScript : InventoryItem
{
    [Header("Scripts:")]
    [SerializeField] private GunMag _mag;
    [SerializeField] private GunVisuals _visuals;
    [SerializeField] private GunAudio _audio;
    [Header("Bullet:")]
    [SerializeField] private GameObject _bullet;
    [Header("Weapon data:")]
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private float _fireRate = .1f;
    [SerializeField] private float _accuracy = 5f;
    [SerializeField] private bool _isFullAuto = true;
    [SerializeField] private int _numberOfBullets = 1;
    [Header("Bullet properties:")]
    [SerializeField] private InventoryItemType _ammoType;
    [SerializeField] private float _damage = 30f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _distance = 20f;

    private GameObject _firePoint;
    private float _nextTimeFire = 0f;
    private bool _fireDown = false;
    private bool _nextFireEnabled = true;
    public UnitHand _unitHand;

    public float Accuracy { get => _accuracy; }
    public float Damage { get => _damage; }
    public float FinalDamage { get => _damage * _numberOfBullets; }
    public WeaponType WeaponType { get => _weaponType; }
    public InventoryItemType AmmoType { get => _ammoType; }

    protected virtual void Start()
    {
        base.Start();

        _firePoint = transform.GetChild(0).gameObject;
        _visuals.MuzzleFlash = _firePoint.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (!_fireDown) return;

        if (Time.time >= _nextTimeFire && _nextFireEnabled)
        {
            if (!_mag.OnFire()) return;
            FireFunction();
            _nextTimeFire = Time.time + _fireRate;
            if (!_isFullAuto) _nextFireEnabled = false;
        }
    }

    public float Reload()
    {
        if (_mag.IsReloading) return -1f;

        int ammoIndex = _unitHand.GetGameObjectByType(_ammoType);
        if (ammoIndex < 0) return -1f;

        _mag.ReloadStart();
        return _mag.ReloadTime;
    }

    public void ReloadEnd()
    {
        int ammoIndex = _unitHand.GetGameObjectByType(_ammoType);
        _unitHand.ThrowItem(ammoIndex);
    }

    public override void Equip(UnitHand hand)
    {
        _unitHand = hand;
        base.Equip();
    }

    public override void Throw()
    {
        _unitHand = null;
        base.Throw();
    }

    public override void UseDown()
    {
        _fireDown = true;
    }

    public override void UseUp()
    {
        _fireDown = false;
        _nextFireEnabled = true;
    }

    public override void Activate()
    {
        base.Activate();

        _mag.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        
        _mag.Deactivate();
    }

    private void FireFunction()
    {
        _visuals.ShowMuzzleFlash(_firePoint);
        _visuals.SmokeOnFire(_damage);
        CreateBullets();
        _visuals.CreateBulletShell(_firePoint);
        _audio.Fire();

        KnockBack();
    }

    private void KnockBack()
    {
        float knockBack = _damage * _numberOfBullets;
        if (knockBack < 50f) knockBack = 50f;
        else if (knockBack > 150f) knockBack = 150f;
        _unitHand.KnockBack(knockBack);
    }

    private void CreateBullets()
    {
        for (int i = 0; i < _numberOfBullets; i++)
        {
            if (_unitHand == null) return;

            var bullet = Instantiate(_bullet, _unitHand.transform);
            bullet.transform.position = _firePoint.transform.position;
            FireAccuracy(bullet, _accuracy);

            bullet.GetComponent<BulletBase>().Damage = _damage * Random.Range(.8f, 1.1f);
            bullet.GetComponent<BulletBase>().Speed = _speed * Random.Range(.8f, 1.1f);
            bullet.GetComponent<BulletBase>().Distance = _distance * Random.Range(.8f, 1.1f);
        }
    }

    private void FireAccuracy(GameObject obj, float accuracy)
    {
        obj.transform.rotation = Quaternion.Euler(0f, transform.localEulerAngles.y, 0f);
        obj.transform.Rotate(0f, Random.Range(-accuracy, accuracy), 0f, Space.Self);
    }

    public override float Progress()
    {
        return _mag.AmmoInPercentage();
    }
}