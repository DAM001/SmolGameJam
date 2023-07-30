using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private ControllerShake _controllerShake;

    public void Shake(Vector3 pos, float intensityMultiplier, float duration)
    {
        _cameraShake.Shake(pos, intensityMultiplier, duration);
        _controllerShake.Rumble(intensityMultiplier, duration);
    }
}
