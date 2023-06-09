using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [Header("Preferences:")]
    [SerializeField] private Vector3 _cameraPosition = new Vector3(0f, 20f, -15f);
    [SerializeField] private float _smoothSpeed = .1f;

    private GameObject _target;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (_target == null) return;

        Vector3 desiredPosition = _target.transform.position + _cameraPosition;
        Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPostion;
    }
}
