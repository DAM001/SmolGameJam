using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Light, Heavy, Shotgun, Sniper }

public class GunScript : InventoryItem
{
    [Header("Scripts:")]
    [SerializeField] private GunMag _mag;
    [SerializeField] private GunVisuals _visuals;
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

    private void Start()
    {
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

    public void Reload()
    {
        if (_mag.IsReloading) return;

        int ammoIndex = _unitHand.GetGameObjectByType(_ammoType);
        if (ammoIndex < 0) return;

        _mag.ReloadStart();
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
        CreateBullets();
        _visuals.CreateBulletShell(_firePoint);
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

    public float AmmoInPercentage()
    {
        return _mag.AmmoInPercentage();
    }
}

/*
    [SerializeField] private GameObject _bullet;
    [SerializeField] private WeaponMag _mag;
    [SerializeField] private AudioSource _audioSource;

    [Header("Weapon data:")]
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private float _fireRate = .1f;
    [SerializeField] private float _accuracy = 5f;
    [SerializeField] private bool _fullAuto = true;
    [SerializeField] private int _numberOfBullets = 1;
    [Header("Bullet properties:")]
    [SerializeField] private float _damage = 30f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _distance = 20f;
    [Header("Visuals:")]
    [SerializeField] private GameObject _shell;
    [Header("Sounds:")]
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _equipSound;

    private GameObject _firePoint;
    private float _nextTimeFire = 0f;
    private bool _fireDown = false;
    private bool _nextFireEnabled = true;
    private GameObject _parent;

    public float Damage { get => _damage; }
    public WeaponType WeaponType { get => _weaponType; }
    public GameObject Parent { get => _parent; }

    private void Start()
    {
        _firePoint = transform.GetChild(0).gameObject;
    }

    public void Activate()
    {
        _mag.Activate();
        EquipSound();
    }

    public void Reload()
    {
        _mag.OnReload();
    }


    private void Update()
    {
        if (!_fireDown) return;

        if (Time.time >= _nextTimeFire && _nextFireEnabled)
        {
            if (!_mag.OnFire()) return;
            _nextTimeFire = Time.time + _fireRate;
            FireFunction();
            if (!_fullAuto) _nextFireEnabled = false;
        }
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

    private void FireFunction()
    {
        StartCoroutine(CreateBullet());

        if (_parent == null) return;
        //_parent.GetComponent<UnitHandItemMovement>().FireEffect();
        StartCoroutine(FireEffect());
        CreateShell();
        FireSound();
    }

    private void FireSound()
    {
        _audioSource.clip = _shootSound;
        _audioSource.pitch = Random.Range(.8f, 1.1f);
        _audioSource.Play();
    }

    private void EquipSound()
    {
        _audioSource.clip = _equipSound;
        _audioSource.pitch = Random.Range(.8f, 1.1f);
        _audioSource.Play();
    }

    private IEnumerator FireEffect()
    {
        _firePoint.transform.GetChild(0).gameObject.SetActive(true);
        _firePoint.transform.GetChild(0).Rotate(0f, 0f, Random.Range(-45f, 45f));
        float scale = Random.Range(.5f, 1f);
        _firePoint.transform.GetChild(0).localScale = new Vector3(scale, scale, .1f);
        yield return new WaitForSeconds(Random.Range(.03f, .08f));
        _firePoint.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void CreateShell()
    {
        GameObject shell = Instantiate(_shell, _firePoint.transform);
        shell.transform.parent = null;
        shell.transform.position = _firePoint.transform.position + transform.forward * -.6f + transform.right * .3f;
        shell.transform.Rotate(Random.Range(2f, 15f), Random.Range(-15f, 15f), 0f);
        shell.GetComponent<Rigidbody>().AddForce(shell.transform.right * Random.Range(200f, 500f));
        Destroy(shell, 5f);
    }

    private IEnumerator CreateBullet()
    {
        for (int i = 0; i < _numberOfBullets; i++)
        {
            FireAccuracy(_accuracy);
            var bullet = Instantiate(_bullet, _firePoint.transform);
            bullet.GetComponent<BulletScript>().Damage = _damage * Random.Range(.8f, 1.1f);
            bullet.GetComponent<BulletScript>().Speed = _speed * Random.Range(.8f, 1.1f);
            bullet.GetComponent<BulletScript>().Distance = _distance * Random.Range(.8f, 1.1f);
            //if (Parent.transform.root.GetComponent<UnitManager>().IsPlayer) bullet.GetComponent<BulletScript>().IsPlayer = true;
            yield return new WaitForFixedUpdate();
        }
    }

    private void FireAccuracy(float accuracy)
    {
        _firePoint.transform.rotation = Quaternion.Euler(0f, transform.localEulerAngles.y, 0f); ;
        _firePoint.transform.Rotate(0f, Random.Range(-accuracy, accuracy), 0f, Space.Self);
    }

    public void SetParent(GameObject parent)
    {
        _parent = parent;
    }

    public bool HasAmmo()
    {
        return _mag.AmmoInPercentage() > 0f;
    }
*/