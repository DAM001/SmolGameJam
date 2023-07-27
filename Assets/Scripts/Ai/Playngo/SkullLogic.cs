using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkullLogic : MonoBehaviour, IKillable
{
    [SerializeField] private NavMeshAgent _navmeshAgent;
    [Header("Properties:")]
    [SerializeField] private float _moveSpeed = 5f;
    [Header("Visuals:")]
    [SerializeField] private GameObject _fireEffect;

    private GameObject _target;

    private void Start()
    {
        _navmeshAgent.speed = _moveSpeed;
    }

    private void FixedUpdate()
    {
        if (_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player");
            _navmeshAgent.SetDestination(transform.position);
            return;
        }

        _navmeshAgent.SetDestination(_target.transform.position);

        if (Vector3.Distance(transform.position, _target.transform.position) < 2f)
        {
            GetComponent<UnitHealth>().Die();
        }
    }

    public void Die()
    {
        Destroy(_fireEffect, 5f);
        _fireEffect.GetComponent<ParticleSystem>().Stop();
        _fireEffect.transform.parent = null;
    }
}
