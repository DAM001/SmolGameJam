using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private CharacterHand _characterHand;
    [Space(10)]
    [SerializeField] private GameObject _cursor;

    private void Update()
    {
        _characterMovement.LookAt(_cursor.transform.position);
    }

    private void OnMovement(InputValue inputValue)
    {
        _characterMovement.Move(inputValue.Get<Vector2>());
    }

    private void OnFireDown()
    {
        _characterHand.OnFireDown();
    }

    private void OnFireUp()
    {
        _characterHand.OnFireUp();
    }

    private void OnInteraction()
    {
        _characterHand.OnEquip();
    }

    private void OnThrowActiveItem()
    {
        _characterHand.OnThrow();
    }

    private void OnItem0()
    {

    }

    private void OnItem1()
    {

    }

    private void OnItem2()
    {

    }

    private void OnItem3()
    {

    }

    private void OnItem4()
    {

    }

    private void OnItem5()
    {

    }
}
