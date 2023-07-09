using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _laserEndLight;
    [Header("Visuals:")]
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _width = .01f;

    private void LateUpdate()
    {
        RaycastHit hit;
        Vector3 endPosition = Physics.Raycast(transform.position, transform.forward, out hit, _range) ? hit.point : transform.position + transform.forward * _range;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, endPosition);

        float distance = (_range - Mathf.Sqrt((transform.position - endPosition).sqrMagnitude)) / _range;
        _lineRenderer.SetWidth(_width, _width * distance);

        if (_laserEndLight.gameObject.activeInHierarchy)
        {
            _laserEndLight.position = endPosition - transform.forward * .05f;
            _laserEndLight.GetComponent<Light>().range = distance < 0f ? 0f : 1f * distance;
        }
    }
}
