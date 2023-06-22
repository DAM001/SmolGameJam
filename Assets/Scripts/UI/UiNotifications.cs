using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiNotifications : MonoBehaviour
{
    [SerializeField] private GameObject _notification;
    [SerializeField] private GameObject _notificationHolder;
    [Header("Properties:")]
    [SerializeField] private float _displayTimer = 3f;

    public void Notify(string text, float time)
    {
        //GameObject notification = Instantiate(_notification, _notificationHolder.transform);
        //notification.transform.parent = _notificationHolder.transform;
        //notification.GetComponent<Notification>().SetInfo(text);
        //notification.GetComponent<Notification>().SetTimer(time);
    }
}
