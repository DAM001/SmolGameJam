using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public enum ControlType { Keyboard, Controller }

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UnitMovement _movement;
    [SerializeField] private UnitHand _characterHand;
    [Space(10)]
    [SerializeField] private GameObject _cursor;

    private Vector2 _lookPos = Vector2.zero;
    private Vector2 _movePos = Vector2.zero;
    private ControlType _controlType;

    public ControlType ControlType { get => _controlType; private set => _controlType = value; }

    private void Update()
    {
        CheckControllerInput();
        CheckKeyboardAndMouseInput();
        CursorVisuals();
    }

    private void CursorVisuals()
    {
        if (_cursor == null)
        {
            _cursor = GameObject.FindGameObjectWithTag("Cursor");
            return;
        }
        if (_controlType == ControlType.Keyboard)
        {
            _movement.MovementScript.LookAt(_cursor.transform.position);
            _cursor.GetComponent<PlayerCursor>().Show();
        }
        else if (_controlType == ControlType.Controller)
        {
            float distance = 10f;
            _cursor.transform.position = transform.position + new Vector3(_lookPos.x * distance, 0f, _lookPos.y * distance);
            _movement.MovementScript.LookAt(_cursor.transform.position);

            if (Vector2.Distance(_lookPos, Vector2.zero) < .1f)
            {
                _cursor.GetComponent<PlayerCursor>().Hide();
                _movement.MovementScript.LookAt(transform.position + new Vector3(_movePos.x, 0f, _movePos.y));
            }
            else _cursor.GetComponent<PlayerCursor>().Show();
        }

        if (_characterHand.HasItem() && _characterHand.CurrentItem.GetComponent<InventoryItem>().ItemType == InventoryItemType.Gun)
        {
            float aimSize = Vector3.Distance(transform.position, _cursor.transform.position) * _characterHand.CurrentItem.GetComponent<GunScript>().Accuracy;
            _cursor.GetComponent<PlayerCursor>().UpdateScale(aimSize);
        }
        else
        {
            _cursor.GetComponent<PlayerCursor>().UpdateScale(25f);
        }
    }

    private void CheckControllerInput()
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad != null && gamepad.allControls.Count > 0)
        {
            foreach (var control in gamepad.allControls)
            {
                if (control is ButtonControl button && button.wasPressedThisFrame)
                {
                    _controlType = ControlType.Controller;
                }
            }
        }
    }

    private void CheckKeyboardAndMouseInput()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;

        if (keyboard != null)
        {
            if (keyboard.anyKey.isPressed)
            {
                _controlType = ControlType.Keyboard;
            }
        }

        if (mouse != null)
        {
            Vector2 mouseMovement = mouse.delta.ReadValue();
            if (mouse.leftButton.isPressed || mouse.rightButton.isPressed || mouseMovement != Vector2.zero)
            {
                _controlType = ControlType.Keyboard;
            }
        }
    }

    //TODO: Rework this
    public void RumbleController(float intensity, float duration)
    {
        StartCoroutine(RumbleHandler(intensity, duration));
    }

    private IEnumerator RumbleHandler(float intensity, float duration)
    {
        Gamepad.current?.SetMotorSpeeds(intensity, intensity);
        yield return new WaitForSeconds(duration);
        Gamepad.current?.SetMotorSpeeds(0f, 0f);
    }

    private void OnMovement(InputValue inputValue)
    {
        _movePos = inputValue.Get<Vector2>();
        _movement.MovementScript.Move(_movePos);
    }

    private void OnLook(InputValue inputValue)
    {
        _lookPos = inputValue.Get<Vector2>();
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
        _characterHand.Interaction();
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
