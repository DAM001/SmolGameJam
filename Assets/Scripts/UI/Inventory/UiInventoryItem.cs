using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiInventoryItem : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _iconImage;

    public void SetBackgroundColor(Color32 color)
    {
        _backgroundImage.color = color;
    }

    public void SetIconImage(Sprite sprite)
    {
        _iconImage.sprite = sprite;
    }

    public void SetIconScale(float scale)
    {
        _iconImage.rectTransform.localScale = new Vector3(scale, scale, scale);
    }
}
