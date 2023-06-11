using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealthAndShield : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider[] _shiledBars;

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
}
