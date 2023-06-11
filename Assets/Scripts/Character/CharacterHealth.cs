using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _maxShield = 150f;

    private float _currentHealth;
    private float _currentShield;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Shielded()
    {
        _currentHealth += _maxHealth / 3f;
        if (_currentHealth > _maxHealth) _currentShield = _maxShield;
    }

    public void Damage(float damage)
    {
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
                return;
            }
        }

        _currentHealth -= damage;
        if (_currentHealth <= 0f) Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}