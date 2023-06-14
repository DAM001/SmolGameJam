using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float _distance = 250f;

    private void Start()
    {
        transform.Rotate(0, Random.Range(-1000f, 1000f), 0);
        transform.position = transform.position + transform.forward * Random.Range(15f, _distance);
    }
}
