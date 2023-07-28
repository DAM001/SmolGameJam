using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private GameObject _anchorObject;
    [Header("Properties:")]
    [SerializeField] private float _intensity = 1f;
    [SerializeField] private float _frequency = 10f;
    [SerializeField] private float _duration = 2f;

    public void Shake(Vector3 pos, float intensityMultiplier, float duration)
    {
        float distance = Vector3.Distance(transform.position, pos);
        float shakeUntilTime = Time.time + duration;

        StartCoroutine(ShakeHandler(distance, shakeUntilTime, intensityMultiplier));
    }

    private IEnumerator ShakeHandler(float distance, float shakeUntilTime, float intensityMultiplier)
    {
        while (shakeUntilTime > Time.time)
        {
            float distanceMultiplier = 1f / distance;
            float intensity = (shakeUntilTime - Time.time) / _duration * _intensity * distanceMultiplier * intensityMultiplier;

            float x = intensity * Mathf.Sin(Time.time * _frequency);
            float y = intensity * Mathf.Sin(Time.time * _frequency * 1.3f);
            float z = intensity * Mathf.Sin(Time.time * _frequency * 0.7f);

            _anchorObject.transform.localPosition += new Vector3(x, y, z);
            yield return new WaitForFixedUpdate();
        }
    }
}
