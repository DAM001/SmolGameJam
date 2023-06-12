using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Parameters:")]
    [SerializeField] private float _aliveTime = 3f;
    [SerializeField] private float _impactForce = 1000f;

    private float _damage = 0f;
    private float _speed = 0f;
    private float _distance = 0f;
    private float _distanceSoFar = 0f;

    public float Damage { set => _damage = value; }
    public float Speed { set => _speed = value; }
    public float Distance { set => _distance = value; }

    private void Start()
    {
        transform.parent = null;
        Destroy(gameObject, _aliveTime);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _speed))
        {
            Collide(hit.transform.gameObject, hit.point);
        }

        _distanceSoFar += _speed;
        transform.position += transform.forward * _speed;

        if (_distanceSoFar > _distance) Destroy(gameObject);
    }

    private void Collide(GameObject hitObj, Vector3 hitPos)
    {
        if (hitObj.gameObject.tag == "Circle") return;

        if (hitObj.GetComponent<CharacterHealth>() != null)
        {
            hitObj.GetComponent<CharacterHealth>().Damage(_damage);
        }

        if (hitObj.GetComponent<Rigidbody>() != null)
        {
            hitObj.GetComponent<Rigidbody>().AddForce(transform.forward * _impactForce);
        }

        Destroy(gameObject);
    }
}
