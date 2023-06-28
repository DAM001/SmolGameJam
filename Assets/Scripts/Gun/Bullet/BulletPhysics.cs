using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPhysics : BulletBase
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speedMultiplier = 1000f;

    protected override void Start()
    {
        base.Start();

        _rigidbody.AddForce(transform.forward * _speed * _speedMultiplier);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollision(collision.gameObject, transform.position);
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
