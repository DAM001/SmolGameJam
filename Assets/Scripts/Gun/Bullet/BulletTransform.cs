using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTransform : BulletBase
{
    [Header("Visuals:")]
    [SerializeField] protected GameObject _hitEffect;

    protected override void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _speed))
        {
            OnCollision(hit.transform.gameObject, hit.point);
        }

        _distanceSoFar += _speed;
        transform.position += transform.forward * _speed;
    }

    protected override void OnCollision(GameObject hitObj, Vector3 hitPos)
    {
        if (hitObj.gameObject.tag == "Circle") return;

        if (hitObj.GetComponent<UnitHealth>() != null)
        {
            float damage = _damage;
            if (hitObj.GetComponent<UnitManager>().IsPlayer) damage *= Random.Range(.5f, .7f);
            hitObj.GetComponent<UnitHealth>().Damage(damage);
            OnCharacterHit();

            if (IsPlayer)
            {
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiRoundInfo>().Damage(damage);
                if (!hitObj.GetComponent<UnitHealth>().IsAlive()) GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiRoundInfo>().Kills(1);
            }
        }

        if (hitObj.GetComponent<Rigidbody>() != null)
        {
            hitObj.GetComponent<Rigidbody>().AddForce(transform.forward * _impactForce);
        }

        Destroy(gameObject);
    }

    private void OnCharacterHit()
    {
        GameObject hitEffect = Instantiate(_hitEffect, transform);
        hitEffect.transform.rotation = transform.rotation;
        hitEffect.transform.Rotate(0f, 180f, 0f);
        hitEffect.transform.parent = null;
        Destroy(hitEffect, 5f);
    }
}
