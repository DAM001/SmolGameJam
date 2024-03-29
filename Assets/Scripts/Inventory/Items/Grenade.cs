using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : InventoryItem
{
    [Header("Grenade:")]
    [SerializeField] private float _activatedThrowForce = 1000f;
    [SerializeField] private float _countdown = 1f;
    [Header("Explotion:")]
    [SerializeField] private Explotion _explotion;

    public override void UseDown()
    {
        EnableRigidbody(true);
        IsEquipped = false;
        Data.Items.Remove(gameObject);

        _rigidbody.AddForce(transform.forward * _activatedThrowForce + transform.up * _activatedThrowForce / 5f);
        _rigidbody.AddTorque(transform.up * Random.Range(-_activatedThrowForce / 10f, _activatedThrowForce / 10f));

        StartCoroutine(ExplotionHandler());
    }

    private IEnumerator ExplotionHandler()
    {
        yield return new WaitForSeconds(_countdown);

        _explotion.Explode();
        Data.Items.Remove(gameObject);
        Destroy(gameObject);
    }
}
