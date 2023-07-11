using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StateFollowPlayer : BaseState
{
    [Header("Properties:")]
    [SerializeField] private float _minDistance = 5f;
    [SerializeField] private float _maxDistance = 30f;
    [Header("States:")]
    [SerializeField] private BaseState _nearState;
    [SerializeField] private BaseState _farState;


    protected GameObject _player;

    public override void EnterState()
    {
        /*_player = GameObject.FindGameObjectWithTag("Player");

        if (_player == null)
        {
            StateMachine.ChangeState(_farState);
        }*/
    }

    public override void PhysicsUpdate()
    {
        Debug.Log("asd");

        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        float distance = Vector3.Distance(transform.position, _player.transform.position);
        StateMachine.UnitMovement.MovementScript.Move(new Vector2(transform.forward.x, transform.forward.z));
        StateMachine.UnitMovement.MovementScript.LookAt(_player.transform.position);

        if (distance < _minDistance)
        {
            StateMachine.ChangeState(_nearState);
        }
        else if (distance > _maxDistance)
        {
            StateMachine.ChangeState(_farState);
        }
    }
}
