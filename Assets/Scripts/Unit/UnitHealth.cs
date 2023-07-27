using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] private GameObject _damagePopup;
    [Space(10)]
    [SerializeField] private float _maxHealth = 100f;
    [Header("Visuals:")]
    [SerializeField] private GameObject _dieEffect;

    private float _currentHealth;

    public bool IsAlive { get => _currentHealth > 0f; }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public bool CanHeal()
    {
        return _currentHealth < _maxHealth;
    }

    public void UseHeal()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(float damage)
    {
        CreateDamagePopup(damage);

        DamageHealth(damage);
    }

    private void DamageHealth(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0f) _currentHealth = 0f;
        if (_currentHealth <= 0f) Die();
    }

    public void Die()
    {
        _currentHealth = 0f;

        DieEffect();
        StartCoroutine(DestroyHandler());
    }

    private IEnumerator DestroyHandler()
    {
        if (GetComponent<IKillable>() != null)
        {
            GetComponent<IKillable>().Die();
        }

        yield return new WaitForFixedUpdate();
        Destroy(gameObject);
    }

    private void CreateDamagePopup(float damage)
    {
        GameObject popup = Instantiate(_damagePopup, transform);
        popup.transform.position = transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(0f, 1f), Random.Range(-2f, 2f));
        popup.GetComponent<DamagePopup>().Damage((int)damage);
    }

    private void DieEffect()
    {
        GameObject hitEffect = Instantiate(_dieEffect, transform);
        hitEffect.transform.rotation = transform.rotation;
        hitEffect.transform.parent = null;
        hitEffect.transform.position += Vector3.up;
        Destroy(hitEffect, 5f);
    }
}