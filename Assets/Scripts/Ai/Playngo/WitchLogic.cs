using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchLogic : MonoBehaviour, IKillable
{
    [SerializeField] protected UnitHand _unitHand;
    [SerializeField] protected UnitMovement unitMovement;
    [SerializeField] protected NavMeshAgent _navmeshAgent;
    [SerializeField] protected float _orbUseTimer = 10f;

    private GameObject _target;

    private void Start()
    {
        StartCoroutine(UseOrb());
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
        unitMovement.MovementScript.Move(new Vector2(transform.forward.x, transform.forward.z));
    }

    private IEnumerator UseOrb()
    {
        yield return new WaitForFixedUpdate();
        _unitHand.Interaction();

        while (true)
        {
            yield return new WaitForSeconds(_orbUseTimer);
            _unitHand.UseDown();
            yield return new WaitForFixedUpdate();
            _unitHand.UseUp();
        }
    }

    public void Die()
    {
        _unitHand.ThrowActiveItem();

        Transform folder = transform.parent;
        transform.parent = null;
        Destroy(folder.gameObject);
    }
}
