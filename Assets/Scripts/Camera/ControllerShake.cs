using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerShake : MonoBehaviour
{
    [SerializeField] private float _intensity = .5f;

    private PlayerController _playerController;

    private void Update()
    {
        if (_playerController != null) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        _playerController = player.GetComponent<PlayerController>();
    }

    public void Rumble(float intensityMultiplier, float duration)
    {
        if (_playerController == null || _playerController.ControlType != ControlType.Controller) return;
        StartCoroutine(RumbleHandler(intensityMultiplier, duration));
    }

    protected IEnumerator RumbleHandler(float intensityMultiplier, float duration)
    {
        Gamepad.current?.SetMotorSpeeds(_intensity * intensityMultiplier, _intensity * intensityMultiplier);
        yield return new WaitForSeconds(duration);
        Gamepad.current?.SetMotorSpeeds(0f, 0f);
    }
}
