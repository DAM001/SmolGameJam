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
        for (int i = 0; i < Data.Characters.Count; i++)
        {
            CheckCharacter(Data.Characters[i]);
        }

        ExplotionForce(_explotionForce);
        ExplotionEffect();
    }

    private void CheckCharacter(GameObject character)
    {
        if (character == null) return;
        if (Vector3.Distance(character.transform.position, transform.position) < _damageRange)
        {
            character.GetComponent<UnitHealth>().Damage(_damage * Random.Range(.7f, 1.3f));
        }
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
