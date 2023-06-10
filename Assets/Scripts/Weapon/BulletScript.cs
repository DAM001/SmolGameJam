using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Parameters:")]
    [SerializeField] private float _damage = 50f;
    [SerializeField] private float _speed = .5f;
    [Space(10)]
    [SerializeField] private float _aliveTime = 3f;
    [SerializeField] private float _impactForce = 1000f;

    public float Damage { set => _damage = value; }
    public float Speed { set => _speed = value; }

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

        transform.position += transform.forward * _speed;
    }

    private void Collide(GameObject hitObj, Vector3 hitPos)
    {
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
