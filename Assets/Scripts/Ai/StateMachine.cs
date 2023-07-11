using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [Header("Scripts:")]
    [SerializeField] private UnitHand _unitHand;
    [SerializeField] private UnitMovement _unitMovement;
    [Header("States:")]
    [SerializeField] private BaseState _startState;

    public UnitHand UnitHand { get => _unitHand; }
    public UnitMovement UnitMovement { get => _unitMovement; }
    public BaseState CurrentState { get; set; }

    private void Start()
    {
        CurrentState = _startState;
        CurrentState.EnterState();
    }

    private void Update()
    {
        if (CurrentState == null) return;

        CurrentState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        if (CurrentState == null) return;

        CurrentState.PhysicsUpdate();
    }

    public void ChangeState(BaseState state)
    {
        CurrentState.ExitState();
        CurrentState = state;
        CurrentState.EnterState();
    }
}
