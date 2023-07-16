using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileController : PlayerController
{
    [SerializeField] private GameObject _mobileController;
    [SerializeField] private GameObject _equipButton;
    [SerializeField] private Joystick _moveJoy;
    [SerializeField] private Joystick _lookJoy;

    public static bool IsMobile = false;

    private void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            IsMobile = true;
            ControlType = ControlType.Mobile;
        }

        IsMobile = true;
        ControlType = ControlType.Mobile;

        _mobileController.SetActive(IsMobile);
        _equipButton.SetActive(false);
    }

    protected override void Update()
    {
        if (IsMobile)
        {
            _movePos = new Vector2(_moveJoy.Horizontal, _moveJoy.Vertical);
            _lookPos = new Vector2(_lookJoy.Horizontal, _lookJoy.Vertical);
        }

        base.Update();
    }
}
