using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f) Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}