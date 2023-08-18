using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkullLogic : MonoBehaviour, IKillable
{
    [SerializeField] private NavMeshAgent _navmeshAgent;
    [SerializeField] private LootSpawner _lootSpawner;
    [Header("Properties:")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _pushDamage = 1000f;
    [Header("Visuals:")]
    [SerializeField] private GameObject _fireEffect;

    private GameObject _target;
    private bool _active = false;

    private void Start()
    {
        _navmeshAgent.speed = _moveSpeed;

        StartCoroutine(DelayActivationHandler());
    }

    private IEnumerator DelayActivationHandler()
    {
        yield return new WaitForSeconds(1f);
        _active = true;
    }

    private void FixedUpdate()
    {
        if (!_active) return;

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

            if (_target.GetComponent<UnitHealth>() == null) return;
            _target.GetComponent<UnitHealth>().Damage(_damage);
        }
    }

    public void Die()
    {
        int numberOfLoot = (int)Random.Range(0, 8) - 4;
        _lootSpawner.Spawn(numberOfLoot);

        Destroy(_fireEffect, 5f);
        _fireEffect.GetComponent<ParticleSystem>().Stop();
        _fireEffect.transform.parent = null;
    }
}
