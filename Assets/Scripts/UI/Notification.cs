using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [SerializeField] private Text _info;
    [SerializeField] private Image _fill;

    private float _time = 3f;

    public void SetTimer(float time)
    {
        _time = time;
        if (time > 5) StartCoroutine(FillHandler());
        else _fill.enabled = false;
        Destroy(gameObject, time);
    }

    public void SetInfo(string info)
    {
        _info.text = info;
    }

    private IEnumerator FillHandler()
    {
        float timer = _time;
        float width = _fill.rectTransform.rect.width;
        while (timer > 0f)
        {
            yield return new WaitForSeconds(.1f);
            timer -= .1f;
            _fill.rectTransform.sizeDelta = new Vector2(timer / _time * width, _fill.rectTransform.sizeDelta.y);
        }
    }
}
