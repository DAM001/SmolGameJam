using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiWeapon : MonoBehaviour
{
    [SerializeField] private Slider _ammoSlider;

    public void UpdateAmmo(float value)
    {
        _ammoSlider.value = value;
    }
}
