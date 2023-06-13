using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandItemMovement : MonoBehaviour
{
    [SerializeField] private CharacterHand _characterHand;
    [Header("Properties:")]
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 10f;

    private void Update()
    {
        if (_characterHand.CurrentItem == null) return;

        Transform itemTransform = _characterHand.CurrentItem.transform;
        Move(itemTransform);
        Rotate(itemTransform);

        if (Vector3.Distance(itemTransform.position, transform.position) > 2f) ResetPosition();
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

    public void FireEffect()
    {
        if (_characterHand.CurrentItem == null) return;
        Transform itemTransform = _characterHand.CurrentItem.transform;
        float multiplier = _characterHand.GetWeapon().Damage / 10f;

        itemTransform.position -= itemTransform.forward * Random.Range(.05f, .2f) * multiplier;
        //itemTransform.Rotate(Random.Range(-1f, -5f) * multiplier, Random.Range(-1f, 1f) * multiplier, 0f, Space.Self);
    }

    public void ResetPosition()
    {
        _characterHand.CurrentItem.transform.position = transform.position;
        _characterHand.CurrentItem.transform.rotation = transform.rotation;
    }
}
