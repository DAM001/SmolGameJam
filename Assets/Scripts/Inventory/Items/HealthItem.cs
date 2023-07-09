using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private InventoryItemType _itemType = InventoryItemType.SmallShield;
    [SerializeField] private float _useTime = 2f;

    private bool _isUsing = false;
    private float _useTimeSlider = 0f;

    private UnitHealth _health;
    private UnitHand _hand;

    public bool IsUsing { get => _isUsing; }

    public void Activate(UnitHand hand, UnitHealth health)
    {
        _health = health;
        _hand = hand;
        _isUsing = false;
    }

    public void OnUse()
    {
        if (_isUsing) return;
        if ((_itemType == InventoryItemType.SmallShield && _health.CanShield()) 
            || (_itemType == InventoryItemType.BigMedkit && _health.CanHeal()))
        {
            StartCoroutine(UseHandler());
        }
    }

    private void Heal()
    {
        if (!_isUsing) return;
        _isUsing = false;

        if (_itemType == InventoryItemType.SmallShield) _health.UseShield();
        else _health.UseHeal();
        //Destroy(_hand.UsedUpItem());
    }

    private IEnumerator UseHandler()
    {
        _isUsing = true;
        _useTimeSlider = 0f;
        while (_useTimeSlider < _useTime)
        {
            yield return new WaitForSeconds(.1f);
            _useTimeSlider += .1f;
        }
        Heal();
    }

    public float Progress()
    {
        return _useTimeSlider / _useTime * 100f;
    }
}
