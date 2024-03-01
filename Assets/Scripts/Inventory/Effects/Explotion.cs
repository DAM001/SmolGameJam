using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    [SerializeField] private float _damage = 100f;
    [SerializeField] private float _damageRange = 5f;
    [Space(10)]
    [SerializeField] private float _explotionForce = 1000f;
    [Header("Visuals:")]
    [SerializeField] private GameObject _explotionEffect;

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _damageRange);
        foreach (Collider hitCollider in colliders)
        {
            UnitHealth unitHealth = hitCollider.GetComponent<UnitHealth>();

            if (unitHealth != null)
            {
                unitHealth.Damage(_damage * Random.Range(.7f, 1.3f));
            }
        }

        ExplotionForce(_explotionForce);
        ExplotionEffect();

        GameObject.FindGameObjectWithTag("CameraFolder").GetComponent<CameraManager>().Shake(transform.position, _damage / 50f, _damageRange / 10f);
    }

    private void ExplotionForce(float force)    
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, _damageRange);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) rb.AddExplosionForce(force, explosionPos, _damageRange, 3f);
        }
    }

    private void ExplotionEffect()
    {
        GameObject hitEffect = Instantiate(_explotionEffect, transform);
        hitEffect.transform.rotation = transform.rotation;
        hitEffect.transform.parent = null;
        hitEffect.transform.position += Vector3.up;
        Destroy(hitEffect, 5f);
    }
}
