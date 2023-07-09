using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : InventoryItem
{
    [SerializeField] private GameObject _light;

    public bool IsTurnedOn { get; private set; }

    private void Start()
    {
        IsTurnedOn = false;
        _light.SetActive(IsTurnedOn);
    }

    public override void UseDown()
    {
        Switch();
    }

    public void Switch()
    {
        IsTurnedOn = !IsTurnedOn;
        _light.SetActive(IsTurnedOn);
    }
}
