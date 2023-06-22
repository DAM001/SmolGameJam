using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHandItemMovement : MonoBehaviour
{
    [SerializeField] private UnitMovement _movement;
    [Header("Properties:")]
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _throwForce = 1000f;

    private GameObject _currentItem;

    private void Update()
    {
        if (_currentItem == null) return;

        Move(_currentItem.transform);
        Rotate(_currentItem.transform);
    }

    private void Move(Transform itemTransform)
    {
        Vector3 desiredPosition = transform.position;
        Vector3 smoothedPostion = Vector3.Lerp(itemTransform.position, desiredPosition, _movementSpeed * Time.deltaTime);
        itemTransform.position = smoothedPostion;
    }

    private void Rotate(Transform itemTransform)
    {
        itemTransform.rotation = Quaternion.Slerp(itemTransform.rotation, transform.rotation, Time.deltaTime * _rotationSpeed);
    }

    public void EquipItem(GameObject item)
    {
        _currentItem = item;
    }

    public void SwitchItem(GameObject item)
    {
        _currentItem = item;
        ResetPosition(item);
    }

    public void ResetPosition(GameObject item)
    {
        item.transform.position = transform.position;
        item.transform.rotation = transform.rotation;
    }

    public void ThrowItem(GameObject item)
    {
        Rigidbody rigidbody = item.GetComponent<InventoryItem>().Rigidbody;
        rigidbody.AddForce(transform.forward * _throwForce + transform.up * _throwForce / 5f);
        rigidbody.AddTorque(transform.up * Random.Range(-_throwForce / 10f, _throwForce / 10f));

        _currentItem = null;
    }

    public void KnockBack(float damage)
    {
        //if (!_characterHand.HasItem()) return;
        //Transform itemTransform = _characterHand.CurrentItem.transform;
        //float multiplier = damage / 10f;

        //itemTransform.position -= itemTransform.forward * Random.Range(.05f, .2f) * multiplier;
        ////itemTransform.Rotate(Random.Range(-1f, -5f) * multiplier, Random.Range(-1f, 1f) * multiplier, 0f, Space.Self);
        //_movement.KnockBack(damage / 100f);
    }
}
