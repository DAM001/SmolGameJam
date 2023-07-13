using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [Header("Parameters:")]
    [SerializeField] protected float _aliveTime = 3f;
    [SerializeField] protected float _impactForce = 1000f;

    protected float _damage = 0f;
    protected float _speed = 0f;
    protected float _distance = 0f;
    protected float _distanceSoFar = 0f;

    public float Damage { set => _damage = value; }
    public float Speed { set => _speed = value; }
    public float Distance { set => _distance = value; }
    public bool IsPlayer { get; set; }

    protected virtual void Start()
    {
        transform.parent = null;
        Destroy(gameObject, _aliveTime);
    }

    protected virtual void FixedUpdate()
    {
        if (_distanceSoFar > _distance) Destroy(gameObject);
    }

    protected virtual void OnCollision(GameObject hitObj, Vector3 hitPos)
    {
        if (hitObj.GetComponent<Rigidbody>() != null)
        {
            hitObj.GetComponent<Rigidbody>().AddForce(transform.forward * _impactForce);
        }

        Destroy(gameObject);
    }
}
