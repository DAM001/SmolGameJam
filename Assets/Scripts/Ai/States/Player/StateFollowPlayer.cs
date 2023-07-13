using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFollowPlayer : BaseState
{
    [Header("Properties:")]
    [SerializeField] private float[] _distances;
    [Header("States:")]
    [SerializeField] protected BaseState[] _states;


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
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        Movement();


    }

    protected void CheckStates()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        for (int i = 0; i < _distances.Length; i++)
        {
            if (distance < _distances[i])
            {
                if (_states[i] != null) StateMachine.ChangeState(_states[i]);
                break;
            }
        }
    }

    protected void Movement()
    {
        StateMachine.UnitMovement.MovementScript.Move(new Vector2(transform.forward.x, transform.forward.z));
        StateMachine.UnitMovement.MovementScript.LookAt(_player.transform.position);
    }
}
