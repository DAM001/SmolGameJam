using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTransform : BulletBase
{
    protected override void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _speed))
        {
            OnCollision(hit.transform.gameObject, hit.point);
        }

        _distanceSoFar += _speed;
        transform.position += transform.forward * _speed;
    }

    protected override void OnCollision(GameObject hitObj, Vector3 hitPos)
    {
        base.OnCollision(hitObj, hitPos);

        Destroy(gameObject);
    }
}
