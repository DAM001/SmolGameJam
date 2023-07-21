using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BawlingPin : InventoryItem
{
    public override void UseDown()
    {
        Throw();

        _rigidbody.velocity = Vector3.zero;
        //transform.rotation = Quaternion.identity;
    }
}
