using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiAmmo : MonoBehaviour
{
    [SerializeField] private GameObject _holder;

    public void UpdateValue(int numberOfAmmo)
    {
        for (int i = 0; i < _holder.transform.childCount; i++)
        {
            _holder.transform.GetChild(i).gameObject.SetActive(i < numberOfAmmo);
        }
    }
}
