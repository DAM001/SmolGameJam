using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public enum ControlType { Keyboard, Controller, Mobile }

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected UnitMovement _movement;
    [SerializeField] protected UnitHand _characterHand;
    [Space(10)]
    [SerializeField] protected GameObject _cursor;

    protected Vector2 _lookPos = Vector2.zero;
    protected Vector2 _movePos = Vector2.zero;
    protected ControlType _controlType;

    public ControlType ControlType { get => _controlType; protected set => _controlType = value; }

    protected virtual void Update()
    {
        if (_controlType != ControlType.Mobile)
        {
            CheckControllerInput();
            CheckKeyboardAndMouseInput();
        }
        CursorVisuals();
    }

    protected void MouseLook()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit, 500, LayerMask.GetMask("Ground")))
        {
            _lookPos = new Vector3(hit.point.x, 0f, hit.point.z);
        }
    }

    protected virtual void CursorVisuals()
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

            MouseLook();
        }
        else if (_controlType == ControlType.Controller)
        {
            LookJoystick();
        }
        else if (_controlType == ControlType.Mobile)
        {
            LookJoystick();

            _movement.MovementScript.Move(_movePos);
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

    protected void LookJoystick()
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

    protected virtual void CheckControllerInput()
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

    protected virtual void CheckKeyboardAndMouseInput()
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

    protected IEnumerator RumbleHandler(float intensity, float duration)
    {
        Gamepad.current?.SetMotorSpeeds(intensity, intensity);
        yield return new WaitForSeconds(duration);
        Gamepad.current?.SetMotorSpeeds(0f, 0f);
    }

    public void OnMovement(InputValue inputValue)
    {
        _movePos = inputValue.Get<Vector2>();
        _movement.MovementScript.Move(_movePos);
    }

    public void OnLook(InputValue inputValue)
    {
        _lookPos = inputValue.Get<Vector2>();
    }

    public void OnMount()
    {
        _characterHand.Mount();
    }

    public void OnUseDown()
    {
        _characterHand.UseDown();
    }

    public void OnUseUp()
    {
        _characterHand.UseUp();
    }

    public void OnReload()
    {
        _characterHand.Reload();
    }

    public void OnInteraction()
    {
        _characterHand.Interaction();
    }

    public void OnThrowActiveItem()
    {
        _characterHand.ThrowActiveItem();
    }

    public void OnScroll(InputValue inputValue)
    {
        float deadZone = .1f;
        float actualValue = inputValue.Get<float>();
        int value = actualValue > deadZone ? 1 : actualValue < -deadZone ? -1 : 0;
        if (value == 0) return;

        _characterHand.Scroll(value);
    }

    public void OnItem0()
    {
        _characterHand.ChangeInventoryIndex(0);
    }

    public void OnItem1()
    {
        _characterHand.ChangeInventoryIndex(1);
    }

    public void OnItem2()
    {
        _characterHand.ChangeInventoryIndex(2);
    }

    public void OnItem3()
    {
        _characterHand.ChangeInventoryIndex(3);
    }

    public void OnItem4()
    {
        _characterHand.ChangeInventoryIndex(4);
    }

    public void OnItem5()
    {
        _characterHand.ChangeInventoryIndex(5);
    }
}
