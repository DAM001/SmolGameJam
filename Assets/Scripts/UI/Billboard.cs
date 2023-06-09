using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void LateUpdate()
    {
        Transform camera = Camera.main.transform;
        transform.rotation = camera.rotation;
    }
}
