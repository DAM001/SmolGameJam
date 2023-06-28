using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private bool _isPlayer;

    public bool IsPlayer { get => _isPlayer; }
}
