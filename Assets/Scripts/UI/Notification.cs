using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [SerializeField] private Text _info;

    public void SetInfo(string info)
    {
        _info.text = info;
    }
}
