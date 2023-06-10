using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class CharacterHandItemMovement : MonoBehaviour
{
    [SerializeField] private CharacterHand _characterHand;
    [Header("Properties:")]
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 10f;

    private void Update()
    {
        if (_characterHand.Weapon == null) return;

        Transform itemTransform = _characterHand.Weapon.transform;
        Move(itemTransform);
        Rotate(itemTransform);
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
        if (_characterHand.Weapon == null) return;
        Transform itemTransform = _characterHand.Weapon.transform;
        float multiplier = _characterHand.GetWeapon().Damage / 10f;

        itemTransform.forward -= itemTransform.forward * Random.Range(1f, 5f) * multiplier;
        itemTransform.Rotate(Random.Range(-1f, -5f) * multiplier, Random.Range(-10f, 10f) * multiplier, 0f, Space.Self);
    }
}
