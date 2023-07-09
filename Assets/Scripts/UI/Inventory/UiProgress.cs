using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiProgress : MonoBehaviour
{
    [SerializeField] private GameObject _progressFolder;
    [SerializeField] private Slider _progressSlider;

    private void Start()
    {
        Show(false);
    }

    public void Show(bool show)
    {
        _progressFolder.SetActive(show);
    }

    public void UpdateProgress(float value)
    {
        _progressSlider.value = value;
    }
}
