using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTopdown : MonoBehaviour
{
    [Header("Preferences:")]
    [SerializeField] private Vector3 _cameraPosition = new Vector3(0f, 20f, -15f);
    [SerializeField] private float _smoothSpeed = .1f;

    private GameObject _target;
    private GameObject _cursor;

    private void Update()
    {
        if (_target == null)
        {
            FindNewTarget();
            return;
        }

        Vector3 desiredPosition = _target.transform.position + _cameraPosition;
        if (_cursor != null)
        {
            desiredPosition = Vector3.Lerp(_target.transform.position, _cursor.transform.position, .2f);
            desiredPosition += _cameraPosition;
        }

        Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPostion;
    }

    public void FindNewTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _target = player;
        }

        GameObject cursor = GameObject.FindGameObjectWithTag("Cursor");
        if (cursor != null)
        {
            _cursor = cursor;
        }
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }
}
