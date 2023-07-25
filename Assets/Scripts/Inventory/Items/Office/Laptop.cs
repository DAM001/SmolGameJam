using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : InventoryItem
{
    [SerializeField] private bool _isOpen = false;
    [Header("Visuals:")]
    [SerializeField] private GameObject _topPart;
    [SerializeField] private float _openRotation = 0f;
    [SerializeField] private float _closeRotation = 0f;

    protected override void Start()
    {
        base.Start();

        UseDown();
    }

    public override void UseDown()
    {
        if (_isOpen) Close();
        else Open();
    }

    public void Open()
    {
        if (_isOpen) return;
        _isOpen = true;

        Rotate(_openRotation);
    }

    public void Close()
    {
        if (!_isOpen) return;
        _isOpen = false;

        Rotate(_closeRotation);
    }

    protected void Rotate(float rotation)
    {
        _topPart.transform.localRotation = Quaternion.identity;
        _topPart.transform.Rotate(rotation, 0f, 0f);
    }
}
