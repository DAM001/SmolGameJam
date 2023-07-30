using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealth : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    private GameObject _player;

    private void Update()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        if (_player.GetComponent<UnitHealth>() == null) return;
        _healthSlider.value = _player.GetComponent<UnitHealth>().CurrentHealth;
    }
}
