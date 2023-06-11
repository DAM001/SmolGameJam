using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiProgress : MonoBehaviour
{
    [SerializeField] private Slider _progressSlider;

    public void UpdateProgress(float value)
    {
        _progressSlider.value = value;
    }
}
