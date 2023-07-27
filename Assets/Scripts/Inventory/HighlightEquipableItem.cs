using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightEquipableItem : MonoBehaviour
{
    [SerializeField] private UnitHand _unitHand;

    private GameObject _equipableItem;

    private void FixedUpdate()
    {
        if (_equipableItem != null)
        {
            ResetOutline(_equipableItem.transform);
            _equipableItem = null;
        }

        GameObject equipableItem = _unitHand.EquipableItem();
        if (equipableItem == null) return;
        _equipableItem = equipableItem;

        HighlightOutline(_equipableItem.transform);
    }

    private void HighlightOutline(Transform transform)
    {
        Outline[] allOutline = transform.GetComponentsInChildren<Outline>();
        foreach (Outline outline in allOutline)
        {
            outline.OutlineColor = new Color32(255, 255, 255, 255);
            outline.OutlineWidth = 4;
        }
    }

    private void ResetOutline(Transform transform)
    {
        Outline[] allOutline = transform.GetComponentsInChildren<Outline>();
        foreach (Outline outline in allOutline)
        {
            outline.OutlineColor = new Color32(0, 0, 0, 255);
            outline.OutlineWidth = 2;
        }
    }
}
