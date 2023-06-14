using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIntro : MonoBehaviour
{
    [Header("Preferences:")]
    [SerializeField] private Vector3 _cameraPosition = new Vector3(0f, 20f, -15f);
    [SerializeField] private float _smoothSpeed = .1f;
    [SerializeField] private Vector3 _targetPos = Vector3.zero;

    private GameObject _target;

    private void Update()
    {
        if (_target == null) return;

        Vector3 desiredPosition = _target.transform.position + _cameraPosition;
        Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPostion;
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public void Move()
    {
        _cameraPosition = _targetPos;
    }
}
