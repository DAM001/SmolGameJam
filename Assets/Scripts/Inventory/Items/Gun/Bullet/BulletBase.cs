using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [Header("Parameters:")]
    [SerializeField] protected float _aliveTime = 3f;
    [SerializeField] protected float _impactForce = 1000f;
    [SerializeField] protected string[] _ignoreCollisionTags;
    [Header("Visuals:")]
    [SerializeField] protected GameObject _bulletImpactEffect;

    protected float _distance = 0f;
    protected float _damage = 0f;
    protected float _speed = 0f;
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
        if (IgnoreCollision(hitObj.tag)) return;

        DoDamage(hitObj, hitPos, _damage);
        ImpactForce(hitObj);
    }

    protected bool IgnoreCollision(string tag)
    {
        for (int i = 0; i < _ignoreCollisionTags.Length; i++)
        {
            if (_ignoreCollisionTags[i] == tag) return true;
        }

        return false;
    }

    protected void DoDamage(GameObject hitObj, Vector3 hitPos, float damage)
    {
        UnitHealth health = hitObj.GetComponent<UnitHealth>();

        if (health == null)
        {
            ImpactEffect(_bulletImpactEffect, hitPos);
            return;
        }

        health.Damage(damage);
        ImpactEffect(health.HitEffect, hitPos);
        UpdatePlayerUi(hitObj, damage);
    }

    protected void ImpactForce(GameObject hitObj)
    {
        Rigidbody rigidbody = hitObj.GetComponent<Rigidbody>();

        if (rigidbody == null) return;
        rigidbody.AddForce(transform.forward * _impactForce);
    }

    protected void UpdatePlayerUi(GameObject hitObj, float damage)
    {
        if (!IsPlayer) return;

        UiRoundInfo info = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiRoundInfo>();
        info.Damage(damage);
        if (!hitObj.GetComponent<UnitHealth>().IsAlive)
        {
            info.Kills(1);
        }
    }

    private void ImpactEffect(GameObject effect, Vector3 pos)
    {
        GameObject hitEffect = Instantiate(effect, transform);
        hitEffect.transform.parent = null;
        hitEffect.transform.rotation = transform.rotation;
        hitEffect.transform.Rotate(0f, 180f, 0f);
        hitEffect.transform.position = pos + hitEffect.transform.forward * .1f;
        Destroy(hitEffect, 5f);
    }
}
