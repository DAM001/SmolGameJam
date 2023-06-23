using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHandItemMovement : MonoBehaviour
{
    [SerializeField] private UnitHand _hand;
    [SerializeField] private UnitMovement _movement;
    [Header("Properties:")]
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _throwForce = 1000f;

    private void Update()
    {
        if (!_hand.HasItem()) return;

        Move(_hand.CurrentItem.transform);
        Rotate(_hand.CurrentItem.transform);
    }

    private void Move(Transform itemTransform)
    {
        Vector3 desiredPosition = _hand.HandObject.transform.position;
        Vector3 smoothedPostion = Vector3.Lerp(itemTransform.position, desiredPosition, _movementSpeed * Time.deltaTime);
        itemTransform.position = smoothedPostion;
    }

    private void Rotate(Transform itemTransform)
    {
        itemTransform.rotation = Quaternion.Slerp(itemTransform.rotation, _hand.HandObject.transform.rotation, Time.deltaTime * _rotationSpeed);
    }

    public void EquipItem(GameObject item)
    {

    }

    public void SwitchItem(GameObject item)
    {
        ResetPosition(item);
    }

    public void ResetPosition(GameObject item)
    {
        item.transform.position = _hand.HandObject.transform.position;
        item.transform.rotation = _hand.HandObject.transform.rotation;
    }

    public void ThrowItem(GameObject item)
    {
        Rigidbody rigidbody = item.GetComponent<InventoryItem>().Rigidbody;
        rigidbody.AddForce(_hand.HandObject.transform.forward * _throwForce + _hand.HandObject.transform.up * _throwForce / 5f);
        rigidbody.AddTorque(_hand.HandObject.transform.up * Random.Range(-_throwForce / 10f, _throwForce / 10f));
    }

    public void KnockBack(float damage)
    {
        if (!_hand.HasItem()) return;
        Transform itemTransform = _hand.CurrentItem.transform;
        float multiplier = damage / 10f;

        itemTransform.position -= itemTransform.forward * Random.Range(.05f, .2f) * multiplier;
        itemTransform.Rotate(Random.Range(-1f, -5f) * multiplier, Random.Range(-1f, 1f) * multiplier, 0f, Space.Self);
        _movement.KnockBack(damage / 100f);
    }
}
