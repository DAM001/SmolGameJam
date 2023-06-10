using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Light, Heavy, Shotgun, Sniper }

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private WeaponMag _mag;

    [Header("Weapon data:")]
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private float _fireRate = .1f;
    [SerializeField] private float _accuracy = 5f;
    [SerializeField] private bool _fullAuto = true;
    [SerializeField] private int _numberOfBullets = 1;
    [Header("Bullet properties:")]
    [SerializeField] private float _damage = 30f;
    [SerializeField] private float _speed = 1f;

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

    public void FireDown()
    {
        _fireDown = true;
    }

    public void FireUp()
    {
        _fireDown = false;
        _nextFireEnabled = true;
    }

    private void FireFunction()
    {
        for (int i = 0; i < _numberOfBullets; i++)
        {
            FireAccuracy(_accuracy);
            var bullet = Instantiate(_bullet, _firePoint.transform);
            bullet.GetComponent<BulletScript>().Damage = _damage;
            bullet.GetComponent<BulletScript>().Speed = _speed;
        }

        if (_parent == null) return;
        _parent.GetComponent<CharacterHandItemMovement>().FireEffect();
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
}
