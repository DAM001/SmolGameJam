using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("Bullet:")]
    [SerializeField] private GameObject _bullet;

    [Header("Weapon data:")]
    [SerializeField] private float _damage = 30f;
    [SerializeField] private float _fireRate = .1f;
    [SerializeField] private float _accuracy = 5f;
    [SerializeField] private bool _fullAuto = true;

    private GameObject _firePoint;
    private float _nextTimeFire = 0f;
    private bool _fireDown = false;
    private bool _nextFireEnabled = true;

    private GameObject _parent;


    public float Damage { get => _damage; }

    private void Start()
    {
        _firePoint = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (!_fireDown) return;

        if (Time.time >= _nextTimeFire && _nextFireEnabled)
        {
            _nextTimeFire = Time.time + _fireRate;
            FireFunction();
        }
    }

    public void FireDown()
    {
        _fireDown = true;
        if (!_fullAuto) _nextFireEnabled = false;
    }

    public void FireUp()
    {
        _fireDown = false;
        _nextFireEnabled = true;
    }

    private void FireFunction()
    {
        FireAccuracy(_accuracy);
        var bullet = Instantiate(_bullet, _firePoint.transform);
        bullet.transform.SetParent(null);

        if (_parent == null) return;
        _parent.GetComponent<CharacterHandItemMovement>().FireEffect();
    }

    private void FireAccuracy(float accuracy)
    {
        _firePoint.transform.rotation = transform.rotation;
        _firePoint.transform.Rotate(0f, Random.Range(-accuracy, accuracy), 0f, Space.Self);
    }

    public void SetParent(GameObject parent)
    {
        _parent = parent;
    }
}
