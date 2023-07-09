using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCounter : MonoBehaviour
{
    [SerializeField] private GameObject _counterFolder;
    [SerializeField] private Text _counterText;

    private void Start()
    {
        Show(false);
    }

    public void Show(bool show)
    {
        _counterFolder.SetActive(show);
    }

    public void UpdateText(string text)
    {
        _counterText.text = text;
    }
}
