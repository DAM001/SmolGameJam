using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCursor : MonoBehaviour
{
    [SerializeField] private GameObject _visuals;
    [Header("Aim:")]
    [SerializeField] private RectTransform _middle;
    [SerializeField] private float _aimSize = 10f;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit, 500, LayerMask.GetMask("Ground")))
        {
            transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
        }
    }

    public void UpdateScale(float size)
    {
        _middle.sizeDelta = new Vector2(_aimSize * size, _aimSize * size);
    }

    public void Hide()
    {
        _visuals.SetActive(false);
    }

    public void Show()
    {
        _visuals.SetActive(true);
    }
}
