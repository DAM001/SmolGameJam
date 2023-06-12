using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public void Notify(string info)
    {
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiNotifications>().Notify(info);
    }
}
