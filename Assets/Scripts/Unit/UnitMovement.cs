using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private MovementBase _unitMovementScript;

    private MovementBase _movementScript;

    public MovementBase MovementScript
    {
        get
        {
            return _movementScript;
        }
        set
        {
            if (value == null)
            {
                _movementScript = _unitMovementScript;
                return;
            }

            _movementScript = value.GetComponent<MovementBase>();
        } 
    }

    private void Start()
    {
        MovementScript = _unitMovementScript;
    }
}
