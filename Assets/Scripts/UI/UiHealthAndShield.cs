using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealthAndShield : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider[] _shiledBars;
    [Header("Indicators:")]
    [SerializeField] private Image _damageIndicator;
    [SerializeField] private Image _shieldIndicator;
    [SerializeField] private Image _circleDamageIndicator;

    private bool _isDamageShown = false;
    private bool _isInCircle = true;

    private void Start()
    {
        _damageIndicator.enabled = false;
        _shieldIndicator.enabled = false;
        _circleDamageIndicator.enabled = false;
    }

    public void UpdateHealth(float current, float max)
    {
        _healthBar.value = current / max * 100f;
    }

    public void UpdateShield(float current, float max)
    {
        float cell = max / 3f;
        for (int i = 0; i < _shiledBars.Length; i++)
        {
            if (current >= cell)
            {
                _shiledBars[i].value = cell;
                current -= cell;
            }
            else
            {
                _shiledBars[i].value = current;
                current = 0;
            }
        }
    }

    public void OutFromCircle(bool value)
    {
        _circleDamageIndicator.enabled = value;
        _isInCircle = !value;
    }

    public void DamageShield()
    {
        if (_isDamageShown) return;
        StartCoroutine(DamageIndicatorHandler(_shieldIndicator));
    }

    public void DamageHealth()
    {
        if (_isDamageShown || !_isInCircle) return;
        StartCoroutine(DamageIndicatorHandler(_damageIndicator));
    }

    private IEnumerator DamageIndicatorHandler(Image indicator)
    {
        _isDamageShown = true;
        indicator.enabled = true;
        yield return new WaitForSeconds(.3f);
        _isDamageShown = false;
        indicator.enabled = false;
    }
}
