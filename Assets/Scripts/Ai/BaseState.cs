using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    [SerializeField] private StateMachine _stateMachine;

    public StateMachine StateMachine { get => _stateMachine; set => _stateMachine = value; }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
}
