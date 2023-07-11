using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : InventoryItem
{
    [Header("Grenade:")]
    [SerializeField] private float _throwForce = 1000f;
    [SerializeField] private float _countdown = 1f;
    [SerializeField] private float _damage = 100f;
    [SerializeField] private float _damageRange = 5f;
    [Space(10)]
    [SerializeField] private float _explotionForce = 1000f;
    [Header("Visuals:")]
    [SerializeField] private GameObject _explotionEffect;

    public override void UseDown()
    {
        EnableRigidbody(true);
        IsEquipped = false;
        Data.Items.Remove(gameObject);

        _rigidbody.AddForce(transform.forward * _throwForce + transform.up * _throwForce / 5f);
        _rigidbody.AddTorque(transform.up * Random.Range(-_throwForce / 10f, _throwForce / 10f));

        StartCoroutine(ExplotionHandler());
    }

    private IEnumerator ExplotionHandler()
    {
        yield return new WaitForSeconds(_countdown);
        Explode();
    }

    private void Explode()
    {
        for (int i = 0; i < Data.Characters.Count; i++)
        {
            CheckCharacter(Data.Characters[i]);
        }

        ExplotionForce(_explotionForce);
        ExplotionEffect();

        Destroy(gameObject);
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
