using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UnitMovement _characterMovement;
    [SerializeField] private UnitHand _characterHand;
    [Space(10)]
    [SerializeField] private GameObject _cursor;

    private void Update()
    {
        if (_cursor == null) _cursor = GameObject.FindGameObjectWithTag("Cursor");
        _characterMovement.LookAt(_cursor.transform.position);
    }

    private void OnMovement(InputValue inputValue)
    {
        _characterMovement.Move(inputValue.Get<Vector2>());
    }

    private void OnUseDown()
    {
        _characterHand.UseDown();
    }

    private void OnUseUp()
    {
        _characterHand.UseUp();
    }

    private void OnReload()
    {
        _characterHand.Reload();
    }

    private void OnInteraction()
    {
        _characterHand.Equip();
    }

    private void OnThrowActiveItem()
    {
        _characterHand.ThrowActiveItem();
    }

    private void OnScroll(InputValue inputValue)
    {
        float deadZone = .1f;
        float actualValue = inputValue.Get<float>();
        int value = actualValue > deadZone ? 1 : actualValue < -deadZone ? -1 : 0;
        if (value == 0) return;

        _characterHand.Scroll(value);
    }

    private void OnItem0()
    {
        _characterHand.ChangeInventoryIndex(0);
    }

    private void OnItem1()
    {
        _characterHand.ChangeInventoryIndex(1);
    }

    private void OnItem2()
    {
        _characterHand.ChangeInventoryIndex(2);
    }

    private void OnItem3()
    {
        _characterHand.ChangeInventoryIndex(3);
    }

    private void OnItem4()
    {
        _characterHand.ChangeInventoryIndex(4);
    }

    private void OnItem5()
    {
        _characterHand.ChangeInventoryIndex(5);
    }
}
