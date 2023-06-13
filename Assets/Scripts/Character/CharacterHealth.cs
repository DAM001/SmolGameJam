using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private CharacterManager _manager;
    [SerializeField] private CharacterHand _hand;
    [SerializeField] private GameObject _damagePopup;
    [Space(10)]
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _maxShield = 150f;
    [Header("Visuals:")]
    [SerializeField] private GameObject _dieEffect;

    private float _currentHealth;
    private float _currentShield;
    private bool _inCircle = true;
    private float _circleDamage = 0f;

    public bool IsInCircle { get => _inCircle; }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _currentShield = _maxShield / 3f;

        UpdateUi();
    }

    private void FixedUpdate()
    {
        if (_inCircle) return;
        DamageHealth(_circleDamage * Time.fixedDeltaTime);
    }

    public void InCircle(bool inCircle, float damage)
    {
        _inCircle = inCircle;
        _circleDamage = damage;

        if (_manager.IsPlayer) GetUi().GetComponent<UiHealthAndShield>().OutFromCircle(!inCircle);
    }

    public bool CanShield()
    {
        return _currentShield < _maxShield;
    }

    public void UseShield()
    {
        _currentShield += _maxShield / 3f;
        if (_currentShield > _maxShield) _currentShield = _maxShield;
        UpdateUi();
    }

    public bool CanHeal()
    {
        return _currentHealth < _maxHealth;
    }

    public void UseHeal()
    {
        _currentHealth = _maxHealth;
        UpdateUi();
    }

    public void Damage(float damage)
    {
        CreateDamagePopup(damage);

        if (_manager.IsPlayer) GetUi().GetComponent<UiHealthAndShield>().DamageShield();

        if (_currentShield > 0f)
        {
            if (_currentShield < damage)
            {
                damage -= _currentShield;
                _currentShield = 0;
            }
            else
            {
                _currentShield -= damage;
                UpdateUi();
                return;
            }
        }

        DamageHealth(damage);
    }

    private void DamageHealth(float damage)
    {
        if (_manager.IsPlayer) GetUi().GetComponent<UiHealthAndShield>().DamageHealth();

        _currentHealth -= damage;
        if (_currentHealth < 0f) _currentHealth = 0f;
        UpdateUi();
        if (_currentHealth <= 0f) Die();
    }

    public void Die()
    {
        DieEffect();
        Notify("A Bobterium died!");
        _hand.ThrowEverything();

        StartCoroutine(DestroyHandler());
    }

    private IEnumerator DestroyHandler()
    {
        yield return new WaitForFixedUpdate();
        Destroy(gameObject);
    }

    private void UpdateUi()
    {
        if (!_manager.IsPlayer) return;

        GetUi().GetComponent<UiHealthAndShield>().UpdateHealth(_currentHealth, _maxHealth);
        GetUi().GetComponent<UiHealthAndShield>().UpdateShield(_currentShield, _maxShield);
    }

    private UiInventory GetUi()
    {
        return GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiInventory>();
    }

    private void Notify(string info)
    {
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiNotifications>().Notify(info);
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