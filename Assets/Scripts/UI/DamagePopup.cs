using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private Text _damageText;
    [SerializeField] private float _aliveTime = 2f;
    [SerializeField] private float _moveUpSpeed = 1f;

    private void Start()
    {
        transform.parent = null;
        Destroy(gameObject, _aliveTime);
    }

    public void Damage(float value)
    {
        _damageText.text = value.ToString();
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.up * _moveUpSpeed * Time.fixedDeltaTime;
    }
}
