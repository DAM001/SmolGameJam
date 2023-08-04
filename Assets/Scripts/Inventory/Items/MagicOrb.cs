using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrb : InventoryItem
{
    [SerializeField] protected SpawnerBase _spawner;
    [Space(10)]
    [SerializeField] protected GameObject _monster;
    [SerializeField] protected int _spawnQuantity = 4;
    [SerializeField] protected float _cooldownTime = 10f;
    [Header("Visuals:")]
    [SerializeField] private GameObject _summonEffect;
    [SerializeField] private GameObject _spawnEffect;

    private bool _canUse = true;

    private float _nextTimeAvailable = 0f;

    protected void Update()
    {
        if (Time.time < _nextTimeAvailable) return;

        _nextTimeAvailable = 0f;
        _canUse = true;
    }

    public override void Activate()
    {
        base.Activate();
    }

    public override void UseDown()
    {
        if (!_canUse) return;
        base.UseDown();

        _nextTimeAvailable = Time.time + _cooldownTime;
        _canUse = false;

        float distance = 3f;
        GameObject[] monsters = _spawner.CreateMultipleObjects(_spawnQuantity, _monster);
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].transform.position += new Vector3(Random.Range(-distance, distance), 0f, Random.Range(-distance, distance));
            CreateEffect(_spawnEffect, monsters[i].transform.position);
            _summonEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    private void CreateEffect(GameObject effect, Vector3 pos)
    {
        GameObject hitEffect = Instantiate(effect, transform);
        hitEffect.transform.parent = null;
        hitEffect.transform.rotation = transform.rotation;
        hitEffect.transform.Rotate(0f, 180f, 0f);
        hitEffect.transform.position = pos + hitEffect.transform.forward * .1f;
        Destroy(hitEffect, 5f);
    }

    public override float Progress()
    {
        return _nextTimeAvailable;
    }

    public override float Cooldown()
    {
        return _cooldownTime;
    }
}
