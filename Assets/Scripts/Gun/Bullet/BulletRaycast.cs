using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletRaycast : BulletBase
{
    [SerializeField] private LineRenderer _lineRenderer;

    protected override void Start()
    {
        base.Start();

        Vector3 endPos = transform.position + transform.forward * _distance;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _distance))
        {
            if (hit.transform != null)
            {
                endPos = hit.point;
                OnCollision(hit.transform.gameObject, hit.point);
            }
        }

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, endPos);

        Destroy(gameObject, .05f);
    }

    protected virtual void OnCollision(GameObject hitObj, Vector3 hitPos)
    {
        if (hitObj.GetComponent<Rigidbody>() != null)
        {
            hitObj.GetComponent<Rigidbody>().AddForce(transform.forward * _impactForce);
        }
    }
}
