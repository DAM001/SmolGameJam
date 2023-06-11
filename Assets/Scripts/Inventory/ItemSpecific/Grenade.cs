using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [Space(10)]
    [SerializeField] private float _throwForce = 1000f;
    [SerializeField] private float _countdown = 1f;
    [SerializeField] private float _damage = 100f;
    [SerializeField] private float _damageRange = 5f;
    [Space(10)]
    [SerializeField] private float _explotionForce = 1000f;

    public void Throw()
    {
        _rigidbody.AddForce(transform.forward * _throwForce + transform.up * _throwForce / 5f);
        _rigidbody.AddTorque(transform.up * Random.Range(-_throwForce / 10f, _throwForce / 10f));

        StartCoroutine(ExplotionHandler());
        gameObject.tag = "Untagged";
        Destroy(gameObject.GetComponent<InventoryItem>());
    }

    private IEnumerator ExplotionHandler()
    {
        yield return new WaitForSeconds(_countdown);
        Explode();
    }

    private void Explode()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Character");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        CheckCharacter(player);
        for (int i = 0; i < gameObjects.Length; i++)
        {
            CheckCharacter(gameObjects[i]);
        }

        ExplotionForce(_explotionForce);

        Destroy(gameObject);
    }

    private void CheckCharacter(GameObject character)
    {
        if (Vector3.Distance(character.transform.position, transform.position) < _damageRange)
        {
            character.GetComponent<CharacterHealth>().Damage(_damage * Random.Range(.7f, 1.3f));
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
}
