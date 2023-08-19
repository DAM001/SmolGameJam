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
        CreateLoot();

        Destroy(_fireEffect, 5f);
        _fireEffect.GetComponent<ParticleSystem>().Stop();
        _fireEffect.transform.parent = null;
    }

    private void CreateLoot()
    {
        int numberOfLoot = Random.Range(0, 10) - 7;
        GameObject[] loot = _lootSpawner.Spawn(numberOfLoot);
        if (loot == null) return;
        for (int i = 0; i < loot.Length; i++)
        {
            loot[i].transform.Rotate(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
            loot[i].GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0f, Random.Range(0f, 500f), 0f));
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.AddForce(transform.forward * 5000f);
        }
    }
}
